using Domain.Models.CampaignModel;
using Domain.Models.TransactionModel;
using Domain.Services.Kafka;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using CampaignModel = Domain.Models.CampaignModel;
using RecurrenceModel = Domain.Models.RecurrenceModel;
using ReferralModel = Domain.Models.ReferralModel;
using TransactionModel = Domain.Models.TransactionModel;

namespace Domain.Processors
{
    public class Processor
    {
        private readonly ILogger<Processor> _logger;
        private readonly ProcessorService _processorService;
        private readonly MessageQueueService _messageQueueService;
        public Processor
            (
                ILogger<Processor> logger,
                ProcessorService processorService,
                MessageQueueService messageQueueService
            )
        {
            _logger = logger;
            _processorService = processorService;
            _messageQueueService = messageQueueService;
        }

        public async Task<bool> ProcessAsync(List<RecurrenceModel.RecurrenceRewardQueueRequest> recurrenceRewardQueueRequests)
        {
            foreach (var recurrenceRewardQueueRequest in recurrenceRewardQueueRequests)
            {
                #region General Check Section
                var referrerCustomer = await _processorService.GetCustomerAsync(recurrenceRewardQueueRequest.Referrer).ConfigureAwait(false);
                if (referrerCustomer == null)
                {
                    _logger.LogInformation("No ReferrerCustomer Found.");
                    continue;
                }
                if (String.IsNullOrEmpty(referrerCustomer.Segments.FirstOrDefault()?.Code))
                {
                    _logger.LogInformation("ReferrerCustomer SegmentCode Not Set.");
                    continue;
                }
                if (referrerCustomer.AccountOpeningDate == null)
                {
                    _logger.LogInformation("ReferrerCustomer AccountOpeningDate Not Set.");
                    continue;
                }
                var refereeCustomer = await _processorService.GetCustomerAsync(recurrenceRewardQueueRequest.Referee).ConfigureAwait(false);
                if (refereeCustomer == null)
                {
                    _logger.LogInformation("No RefereeCustomer Found.");
                    continue;
                }
                var referrals = GetReferralDetail(recurrenceRewardQueueRequest);
                if (referrals == null || !referrals.Any())
                {
                    _logger.LogInformation("No Referral Found.");
                    continue;
                }
                var referralWithCompleteStatus = referrals?.FirstOrDefault(o => o.ReferralStatus == 1);
                if (referralWithCompleteStatus == null || referralWithCompleteStatus.Id == 0)
                {
                    _logger.LogInformation("Referral Not Completed For Request : {0}", JsonConvert.SerializeObject(recurrenceRewardQueueRequest));
                    continue;
                }
                var recurrenceReward = await _processorService.GetRecurrenceRewardAsync(recurrenceRewardQueueRequest).ConfigureAwait(false);
                if (recurrenceReward == null)
                {
                    _logger.LogInformation("No RecurrenceReward Found.");
                    continue;
                }
                if (recurrenceReward.RewardCapping == recurrenceReward.RewardedValue)
                {
                    _logger.LogInformation("Rewarded Till Capping Limit");
                    continue;
                }
                if (!String.Equals(referralWithCompleteStatus.CampaignId, recurrenceReward.CumulativeCampaignId, StringComparison.Ordinal))
                {
                    _logger.LogInformation("CampaignId Not Matched.");
                    continue;
                }
                var transaction = await _processorService.GetTransactionAsync(recurrenceRewardQueueRequest.TransactionId).ConfigureAwait(false);
                if (transaction == null)
                {
                    _logger.LogInformation("No Transaction Found.");
                    continue;
                }
                var campaign = await _processorService.GetCampaignAsync(recurrenceReward.CumulativeCampaignId).ConfigureAwait(false);
                if (campaign == null)
                {
                    _logger.LogInformation("No Campaign Found.");
                    continue;
                }
                #endregion

                ProcessedTransaction processedTransaction = new ProcessedTransaction()
                {
                    TransactionRequest = transaction,
                    Customer = refereeCustomer,
                    IsReferred = true,
                    ReferralDetail = new ReferralModel.ReferralDetail() { ReferrerCustomer = referrerCustomer, Referral = referralWithCompleteStatus }
                };
                #region Core Qualification Section
                if (campaign.Filter.IsRecurrenceReward)
                {
                    var configuredRecurrenceReward = campaign.RewardCriteria.ReferralProgram.RecurrenceReward;

                    var recurrenceRewardCapping = recurrenceReward.RewardCapping;
                    var recurrenceRewardValue = recurrenceReward.RewardedValue;

                    if (configuredRecurrenceReward != null)
                    {
                        if (configuredRecurrenceReward.IsDynamicSegment)
                        {

                            var configuredRecurrenceDynamicSegment = configuredRecurrenceReward.DynamicSegments.Where(o => o.DynamicSegmentCode == referrerCustomer.Segments.FirstOrDefault()?.Code).FirstOrDefault();
                            if (configuredRecurrenceDynamicSegment == null)
                            {
                                _logger.LogInformation("ConfiguredRecurrenceDynamicSegment Not Found.");
                                continue;
                            }
                            var configuredRecurrenceAdditionalCondition = configuredRecurrenceDynamicSegment.AdditionalCondition;
                            if (!((((DateTime)referrerCustomer.AccountOpeningDate).AddDays(configuredRecurrenceAdditionalCondition.AdditionalConditionDuration.Value)).Date >= DateTime.Now.Date))
                            {
                                _logger.LogInformation("One Year Elasped From Account Opening.");
                                continue;
                            }
                            var isRewardingApplyOnlyOnBrokrageAmount = configuredRecurrenceAdditionalCondition.IsRewardingApplyOnlyOnBrokrageAmount;
                            var amount = transaction.TransactionDetail.Amount;
                            if (isRewardingApplyOnlyOnBrokrageAmount)
                            {
                                var flag = decimal.TryParse(transaction.InvestmentDetails?.BrokerageAmount, out decimal brokrageAmount);
                                if (flag)
                                {
                                    amount = (double)brokrageAmount;
                                }
                            }
                            var cashbackRewardOption = configuredRecurrenceDynamicSegment.RewardOptions.Where(o => String.Equals(o.RewardType, "Cashback", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                            if (cashbackRewardOption == null)
                            {
                                _logger.LogInformation("No Cashback RewardOption Configured.");
                                continue;
                            }
                            var rewardValue = ((decimal)amount) * (cashbackRewardOption.Cashback.CashBackPercentage.Value / 100);
                            if (cashbackRewardOption.Cashback.CashBackPercentage.MaximumCashback > 0)
                            {
                                if (rewardValue > cashbackRewardOption.Cashback.CashBackPercentage.MaximumCashback)
                                {
                                    rewardValue = cashbackRewardOption.Cashback.CashBackPercentage.MaximumCashback;
                                }
                            }
                           
                            if ((recurrenceReward.RewardedValue + rewardValue) > recurrenceReward.RewardCapping)
                            {
                                _logger.LogInformation("Capping Limit Exceed.");
                                continue;
                            }

                            processedTransaction.MatchedCampaigns = new List<TransactionModel.MatchedCampaign>() { SetMatchedCampaign(campaign, true, false, false, transaction.EventId, transaction.ChildEventCode, cashbackRewardOption) };
                            if (rewardValue > 0)
                            {
                                processedTransaction.IsRecurrenceReward = true;
                                processedTransaction.RecurrenceRewardDetail = new RecurrenceModel.RecurrenceRewardDetail() { RewardType = "CASHBACK", RewardOption = cashbackRewardOption, RewardValue = rewardValue };
                                processedTransaction.TransactionRequest.ChildEventCode = "RC";
                                PublishInKafka(processedTransaction, "OfferMap_IIFL_TEST");
                            }
                            
                        }

                    }
                }
                #endregion

            }
            return false;

            static TransactionModel.MatchedCampaign SetMatchedCampaign(CampaignModel.EarnCampaign campaign, bool isDirect, bool isLock, bool isUnlock, string eventName, string childEventCode, RewardOption rewardOption)
            {
                return new TransactionModel.MatchedCampaign
                {
                    IsDirect = isDirect,
                    IsLock = isLock,
                    IsUnLock = isUnlock,
                    CampaignId = campaign.Id,
                    EventType = eventName,
                    ChildEventCode = childEventCode,
                    OfferType = campaign.OfferType,
                    RewardCriteria = campaign.RewardCriteria,
                    RewardOptions = new List<RewardOption>() { rewardOption },
                    StartDate = campaign.StartDate,
                    EndDate = campaign.EndDate,
                    Narration = isLock ? campaign.Content.UnlockCondition : campaign.Content.RewardNarration,
                    IsOncePerCampaign = campaign.OncePerCampaign,
                    OnceInLifeTime = campaign.OnceInLifeTime,
                    CTAUrl = campaign.Content.CTAUrl,
                    Filter = campaign.Filter,
                    ApplyProductCode = campaign.ApplyProductCode,
                    ProductCode = campaign.ProductCode
                };
            }
        }

        #region Private Section
        private List<ReferralModel.Referral> GetReferralDetail(RecurrenceModel.RecurrenceRewardQueueRequest recurrenceRewardQueueRequest)
        {
            ReferralModel.ReferralRequest referralRequest = new ReferralModel.ReferralRequest()
            {
                Referee = new ReferralModel.Referee() { Lob = recurrenceRewardQueueRequest.Referee?.Lob, CustomerId = recurrenceRewardQueueRequest.Referee?.CustomerId }
            };
            var referrals = _processorService.GetReferralDetail(referralRequest);
            return referrals.Where(o => o.Status == 1).ToList();
        }
        private void PublishInKafka(TransactionModel.ProcessedTransaction transaction, string topicName) => _messageQueueService.ProduceTransaction(topicName, Guid.NewGuid().ToString(), JsonConvert.SerializeObject(transaction));
        #endregion
    }
}
