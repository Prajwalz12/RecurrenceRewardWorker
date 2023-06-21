using Domain.Models.CampaignModel;
using Extensions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using CampaignModel = Domain.Models.CampaignModel;
using CustomerTimelineModel = Domain.Models.CustomerTimelineModel;
using TransactionModel = Domain.Models.TransactionModel;
using ReferralRuleModel = Domain.Models.ReferralRuleModel;
using Domain.Models.TransactionModel;
using RecurrenceModel = Domain.Models.RecurrenceModel;

namespace Domain.Builders
{
    public static class QueryBuilder
    {
        public static FilterDefinition<CampaignModel.EarnCampaign> PrepareFilterQueryWithCollection(this TransactionModel.ProcessedTransaction processedTransaction, bool isNewCustomer)
        {
            List<FilterDefinition<CampaignModel.EarnCampaign>> filters = new List<FilterDefinition<CampaignModel.EarnCampaign>>();
            var filterQueryBuilder = Builders<CampaignModel.EarnCampaign>.Filter;
            filters.Add(PublishedCampaignFilter());
            filters.Add(ActiveCampaignFilter());
            filters.Add(LobFilter());
            filters.Add(ChannelCodeFilter());
            if (isNewCustomer)
            {
                filters.Add(CampaignForCustomerTypeFilter("NEW"));
            }
            else
            {
                filters.Add(CampaignForCustomerTypeFilter("EXISTING"));
            }
            // Open When 
            //filters.Add(RewardOptionFilter());
            //filters.Add(TripleRewardFilter());
            //filters.Add(filterQueryBuilder.Where(o=> o.CampaignName == "dfsddfsfsdf" || o.CampaignName == "Voucher4"));
            if (!(processedTransaction.TransactionRequest.EventId == "AO" || processedTransaction.TransactionRequest.EventId == "LOGIN"))
            {
                if (!String.IsNullOrEmpty(processedTransaction.TransactionRequest.TransactionDetail.TransactionMode))
                {
                    filters.Add(TransactionModeFilter());
                }
            }
            if (processedTransaction.TransactionRequest.TransactionDetail.DateTime != null)
            {
                filters.Add(StartDateFilter());
                filters.Add(EndDateFilter());
            }

            //if (!String.IsNullOrEmpty(processedTransaction.TransactionRequest.TransactionDetail.ProductCode))
            //{
            //    filters.Add(ProductCodeFilter());
            //}

            filters.Add(CustomerTypeFilter());

            if (IsTransactionHasCampaign() && TransactionCampaignRewardFlag() && IsTransactionHasCamgaignId())
            {
                filters.Add(CampaignIdFilter());
            }
            else
            {
                filters.Add(OfferTypeFilter());
            }

            var filterQueryDefinition = GetCombinedFilterWithAnd(filters);

            return filterQueryDefinition;

            #region Local Function
            //FilterDefinition<CampaignModel.EarnCampaign> CampaignIdNotNullOrEmptyFilter() => filterQueryBuilder.Where(o => !String.IsNullOrEmpty(o.Id));
            //FilterDefinition<CampaignModel.EarnCampaign> CampaignIdFilter() => filterQueryBuilder.Where(o => o.Id == processedTransaction.TransactionRequest.Campaign.Id);
            FilterDefinition<CampaignModel.EarnCampaign> CampaignIdFilter() => filterQueryBuilder.Where(o => o.IIFLCampaignId == processedTransaction.TransactionRequest.Campaign.Id);

            FilterDefinition<CampaignModel.EarnCampaign> OfferTypeFilter()
            {
                List<FilterDefinition<CampaignModel.EarnCampaign>> localFilters = new List<FilterDefinition<CampaignModel.EarnCampaign>>();
                var localFilterQueryBuilder = Builders<CampaignModel.EarnCampaign>.Filter;
                //localFilters.Add(localFilterQueryBuilder.Where(o => o.OfferType == "Hybrid"));
                //if (String.Equals(processedTransaction.TransactionRequest.EventId, "Spend", StringComparison.OrdinalIgnoreCase))
                if (String.Equals(processedTransaction.TransactionRequest.EventId, "PAYMENT", StringComparison.OrdinalIgnoreCase))
                {
                    localFilters.Add(localFilterQueryBuilder.Where(o => o.OfferType == "PAYMENT"));

                    //if (String.Equals(processedTransaction.TransactionRequest.LOB, "H45-REMUPI", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    if (String.Equals(processedTransaction.TransactionRequest.TransactionDetail.Type, "p2m", StringComparison.OrdinalIgnoreCase))
                    //    {
                    //        localFilters.Add(localFilterQueryBuilder.Where(o => o.OfferType == "TripleReward"));
                    //    }
                    //}
                }
                else
                {
                    localFilters.Add(localFilterQueryBuilder.Where(o => o.OfferType == "ACTIVITY"));
                }
                return GetCombinedFilterWithOr(localFilters);
                //filterQueryBuilder.Where(o => o.OfferType == GetOfferType(processedTransaction.TransactionRequest.EventId));
            }

            FilterDefinition<CampaignModel.EarnCampaign> ActiveCampaignFilter() => filterQueryBuilder.Where(o => o.Status == "ACTIVE");
            FilterDefinition<CampaignModel.EarnCampaign> ApprovedCampaignFilter() => filterQueryBuilder.Where(o => o.Status == "APPROVED");

            FilterDefinition<CampaignModel.EarnCampaign> PublishedCampaignFilter() => filterQueryBuilder.Where(o => o.IsPublished);

            //bool IsPaymentInstrumentNullOrEmpty() => String.IsNullOrEmpty(processedTransaction.TransactionRequest.TransactionDetail.PaymentInstrument);
            FilterDefinition<CampaignModel.EarnCampaign> LobFilter() => filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB);

            bool IsTransactionHasCamgaignId() => !string.IsNullOrEmpty(processedTransaction.TransactionRequest.Campaign.Id);
            bool TransactionCampaignRewardFlag() => processedTransaction.TransactionRequest.Campaign.RewardedFlg;

            bool IsTransactionHasCampaign() => typeof(TransactionModel.Transaction).HasProperty("Campaign") && processedTransaction.TransactionRequest.Campaign != null;
            //bool IsApplyLobFraudFilter() => (typeof(CustomerModel.Customer).HasProperty("Flags") && typeof(Common.CustomerModel.Flag).HasProperty("LobFraud") && processedTransaction.Customer.Flags.LobFraud.Any());
            FilterDefinition<CampaignModel.EarnCampaign> RewardOptionFilter() => filterQueryBuilder.Where(o => o.RewardOption.Count == 1);
            FilterDefinition<CampaignModel.EarnCampaign> CustomerTypeFilter() => filterQueryBuilder.Where(o => o.CustomerType.Contains(processedTransaction.Customer.Type.ToUpper()));
            FilterDefinition<CampaignModel.EarnCampaign> StartDateFilter() => filterQueryBuilder.Where(o => o.StartDate <= processedTransaction.TransactionRequest.TransactionDetail.DateTime);
            FilterDefinition<CampaignModel.EarnCampaign> EndDateFilter() => filterQueryBuilder.Where(o => ((o.EndDate != null && o.EndDate >= processedTransaction.TransactionRequest.TransactionDetail.DateTime) || (o.UnLockExpiryDate != null && o.UnLockExpiryDate >= processedTransaction.TransactionRequest.TransactionDetail.DateTime)));
            FilterDefinition<CampaignModel.EarnCampaign> TripleRewardFilter() => filterQueryBuilder.Where(o => o.Filter.IsTripleReward == false);
            FilterDefinition<CampaignModel.EarnCampaign> ChannelCodeFilter() => filterQueryBuilder.Where(o => o.Channel.Contains(processedTransaction.TransactionRequest.ChannelCode.ToUpper()));
            FilterDefinition<CampaignModel.EarnCampaign> TransactionModeFilter() => filterQueryBuilder.Where(o => o.Mode.Contains(processedTransaction.TransactionRequest.TransactionDetail.TransactionMode));
            FilterDefinition<CampaignModel.EarnCampaign> ProductCodeFilter() => filterQueryBuilder.Where(o => o.ProductCode.Contains(processedTransaction.TransactionRequest.TransactionDetail.ProductCode));
            FilterDefinition<CampaignModel.EarnCampaign> CampaignForCustomerTypeFilter(string customerType) => filterQueryBuilder.Where(o => o.CustomerType.Contains(customerType));
            #endregion
        }



        public static FilterDefinition<TransactionModel.Transaction> PrepareTransactionFilterForLoanDisbursalQuery(this TransactionModel.ProcessedTransaction processedTransaction, DateTime? startDate, DateTime? endDate)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Type == processedTransaction.TransactionRequest.TransactionDetail.Type));
            //filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.LoanData.DisbursalType == configuredDisbursalType));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.LoanData.DisbursalType == processedTransaction.TransactionRequest.TransactionDetail.LoanData.DisbursalType));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.LoanType.Type == processedTransaction.TransactionRequest.TransactionDetail.LoanType.Type));
            filters.Add(filterQueryBuilder.Where(o => startDate <= o.TransactionDetail.DateTime));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= endDate));

            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<TransactionModel.Transaction> PrepareTransactionFilterForEMIRepaymentQuery(this TransactionModel.ProcessedTransaction processedTransaction, DateTime? startDate, DateTime? endDate)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => startDate <= o.TransactionDetail.DateTime));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= endDate));
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Type == processedTransaction.TransactionRequest.TransactionDetail.Type));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.EmiType.Type == processedTransaction.TransactionRequest.TransactionDetail.EmiType.Type));
            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<TransactionModel.Transaction> PrepareTransactionFilterForSignUpQuery(this TransactionModel.ProcessedTransaction processedTransaction)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<TransactionModel.Transaction> PrepareTransactionFilterForLoginQuery(this TransactionModel.ProcessedTransaction processedTransaction, CampaignModel.EarnCampaign campaign)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime >= (DateTime)campaign.StartDate));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= (DateTime)campaign.EndDate));

            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<TransactionModel.Transaction> PrepareTransactionFilterForWatchlistCreationQuery(this TransactionModel.ProcessedTransaction processedTransaction)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            //filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime >= (DateTime)campaign.StartDate));
            //filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= (DateTime)campaign.EndDate));

            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<TransactionModel.Transaction> PrepareTransactionFilterForAccountOpeningQuery(this TransactionModel.ProcessedTransaction processedTransaction)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<CustomerTimelineModel.CustomerTimeline> PrepareFilterForCustomerTimelineQuery(this CustomerTimelineModel.CustomerTimelineRequest customerTimelineRequest)
        {
            List<FilterDefinition<CustomerTimelineModel.CustomerTimeline>> filters = new List<FilterDefinition<CustomerTimelineModel.CustomerTimeline>>();
            FilterDefinitionBuilder<CustomerTimelineModel.CustomerTimeline> filterQueryBuilder = Builders<CustomerTimelineModel.CustomerTimeline>.Filter;
            if (customerTimelineRequest.Referrer != null)
            {
                var referrer = customerTimelineRequest.Referrer;
                if (!String.IsNullOrEmpty(referrer.Lob))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referrer.Lob == referrer.Lob));
                }
                if (!String.IsNullOrEmpty(referrer.CustomerId))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referrer.CustomerId == referrer.CustomerId));
                }
            }
            if (customerTimelineRequest.Referee != null)
            {
                var referee = customerTimelineRequest.Referee;
                if (!String.IsNullOrEmpty(referee.Lob))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referee.Lob == referee.Lob));
                }
                if (!String.IsNullOrEmpty(referee.CustomerId))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referee.CustomerId == referee.CustomerId));
                }
                if (!String.IsNullOrEmpty(referee.MobileNumber))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referee.MobileNumber == referee.MobileNumber));
                }
            }
            if (!String.IsNullOrEmpty(customerTimelineRequest.TimelineEventCode))
            {
                filters.Add(filterQueryBuilder.Where(o => o.CustomerTimelineEvent.EventCode == customerTimelineRequest.TimelineEventCode));
            }
            //if (customerTimelineRequest.Status != null)
            //{
            //    filters.Add(filterQueryBuilder.Where(o => o.Status == (bool)customerTimelineRequest.Status));
            //}
            return GetCombinedFilterWithAnd<CustomerTimelineModel.CustomerTimeline>(filters);
        }
        public static FilterDefinition<ReferralRuleModel.ReferralRule> PrepareFilterForReferralRuleQuery(ReferralRuleModel.ReferralRuleRequest referralRuleRequest)
        {
            List<FilterDefinition<ReferralRuleModel.ReferralRule>> filters = new List<FilterDefinition<ReferralRuleModel.ReferralRule>>();
            FilterDefinitionBuilder<ReferralRuleModel.ReferralRule> filterQueryBuilder = Builders<ReferralRuleModel.ReferralRule>.Filter;
            if (!String.IsNullOrEmpty(referralRuleRequest.LobCode))
            {
                filters.Add(filterQueryBuilder.Where(o => o.Lob == referralRuleRequest.LobCode));
            }
            if (!String.IsNullOrEmpty(referralRuleRequest.DynamicSegmentCode))
            {
                filters.Add(filterQueryBuilder.Where(o => o.DynamicSegment == referralRuleRequest.DynamicSegmentCode));
            }
            if (!String.IsNullOrEmpty(referralRuleRequest.ReferralRuleId))
            {
                filters.Add(filterQueryBuilder.Where(o => o.ReferralRuleId == referralRuleRequest.ReferralRuleId));
            }
            return GetCombinedFilterWithAnd<ReferralRuleModel.ReferralRule>(filters);
        }
        private static FilterDefinition<EarnCampaign> GetCombinedFilterWithAnd(List<FilterDefinition<EarnCampaign>> filters)
        {
            var combinedFilter = filters[0];
            if (filters.Count == 1)
            {
                return filters[0];
            }
            for (int i = 1; i < filters.Count; i++)
            {
                combinedFilter &= filters[i];
            }
            return combinedFilter;
        }
        private static FilterDefinition<T> GetCombinedFilterWithAnd<T>(List<FilterDefinition<T>> filters)
        {
            var combinedFilter = filters[0];
            if (filters.Count == 1)
            {
                return filters[0];
            }
            for (int i = 1; i < filters.Count; i++)
            {
                combinedFilter &= filters[i];
            }
            return combinedFilter;
        }
        private static FilterDefinition<EarnCampaign> GetCombinedFilterWithOr(List<FilterDefinition<EarnCampaign>> filters)
        {
            var combinedFilter = filters[0];
            if (filters.Count == 1)
            {
                return filters[0];
            }
            for (int i = 1; i < filters.Count; i++)
            {
                combinedFilter |= filters[i];
            }
            return combinedFilter;
        }
        private static FilterDefinition<T> GetCombinedFilterWithOr<T>(List<FilterDefinition<T>> filters)
        {
            var combinedFilter = filters[0];
            if (filters.Count == 1)
            {
                return filters[0];
            }
            for (int i = 1; i < filters.Count; i++)
            {
                combinedFilter |= filters[i];
            }
            return combinedFilter;
        }
        private static FilterDefinition<TransactionModel.Transaction> GetCombinedFilterWithAnd(List<FilterDefinition<TransactionModel.Transaction>> filters)
        {
            FilterDefinition<TransactionModel.Transaction> combinedFilter = filters[0];
            if (filters.Count == 1)
            {
                return filters[0];
            }
            for (int i = 1; i < filters.Count; i++)
            {
                combinedFilter &= filters[i];
            }
            return combinedFilter;
        }
        private static FilterDefinition<TransactionModel.Transaction> GetCombinedFilterWithOr(List<FilterDefinition<TransactionModel.Transaction>> filters)
        {
            FilterDefinition<TransactionModel.Transaction> combinedFilter = filters[0];
            if (filters.Count == 1)
            {
                return filters[0];
            }
            for (int i = 1; i < filters.Count; i++)
            {
                combinedFilter |= filters[i];
            }
            return combinedFilter;
        }

        public static FilterDefinition<Transaction> PrepareTransactionFilterForSubscriptionQuery(ProcessedTransaction processedTransaction, DateTime startDate, DateTime? endDate)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => startDate <= o.TransactionDetail.DateTime));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= endDate));
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Type == processedTransaction.TransactionRequest.TransactionDetail.Type));
            filters.Add(filterQueryBuilder.Where(o => o.InvestmentDetails.InvestorType == processedTransaction.TransactionRequest.InvestmentDetails.InvestorType));
            if (!String.IsNullOrEmpty(processedTransaction.TransactionRequest.InvestmentDetails.SubscriptionType))
            {
                filters.Add(filterQueryBuilder.Where(o => o.InvestmentDetails.SubscriptionType == processedTransaction.TransactionRequest.InvestmentDetails.SubscriptionType));
            }
            return GetCombinedFilterWithAnd(filters);
        }

        public static FilterDefinition<Transaction> PrepareTransactionFilterForTradingQuery(ProcessedTransaction processedTransaction, DateTime startDate, DateTime? endDate)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => startDate <= o.TransactionDetail.DateTime));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= endDate));
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Type == processedTransaction.TransactionRequest.TransactionDetail.Type));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.TradingType.Type == processedTransaction.TransactionRequest.TransactionDetail.TradingType.Type));
            return GetCombinedFilterWithAnd(filters);
        }

        public static FilterDefinition<Transaction> PrepareTransactionFilterForInvestmentQuery(ProcessedTransaction processedTransaction, DateTime startDate, DateTime? endDate)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => startDate <= o.TransactionDetail.DateTime));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= endDate));
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Type == processedTransaction.TransactionRequest.TransactionDetail.Type));
            filters.Add(filterQueryBuilder.Where(o => o.InvestmentDetails.InvestmentType == processedTransaction.TransactionRequest.InvestmentDetails.InvestmentType));
            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<Transaction> PrepareTransactionFilterForFundTransferQuery(ProcessedTransaction processedTransaction, DateTime startDate, DateTime? endDate)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime >= startDate));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= endDate));
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Type == processedTransaction.TransactionRequest.TransactionDetail.Type));
            filters.Add(filterQueryBuilder.Where(o => o.InvestmentDetails.TransferType == processedTransaction.TransactionRequest.InvestmentDetails.TransferType));
            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<Transaction> PrepareTransactionFilterForPremiumQuery(ProcessedTransaction processedTransaction, DateTime startDate, DateTime? endDate)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime >= startDate));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= endDate));
            filters.Add(filterQueryBuilder.Where(o => o.EventId == processedTransaction.TransactionRequest.EventId));
            filters.Add(filterQueryBuilder.Where(o => o.LOB == processedTransaction.TransactionRequest.LOB));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == processedTransaction.TransactionRequest.TransactionDetail.Customer.CustomerId));
            filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Type == processedTransaction.TransactionRequest.TransactionDetail.Type));
            filters.Add(filterQueryBuilder.Where(o => o.InvestmentDetails.PremiumType == processedTransaction.TransactionRequest.InvestmentDetails.PremiumType));
            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<TransactionModel.Transaction> PrepareTransactionFilterForTimelineEventQuery(TransactionModel.TimelineTransactionRequest timelineTransactionRequest)
        {
            List<FilterDefinition<TransactionModel.Transaction>> filters = new List<FilterDefinition<TransactionModel.Transaction>>();
            var filterQueryBuilder = Builders<TransactionModel.Transaction>.Filter;

            if (!String.IsNullOrEmpty(timelineTransactionRequest.Lob))
            {
                filters.Add(filterQueryBuilder.Where(o => o.LOB == timelineTransactionRequest.Lob));
            }
            if (!String.IsNullOrEmpty(timelineTransactionRequest.CustomerId))
            {
                filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Customer.CustomerId == timelineTransactionRequest.CustomerId));
            }

            if (!String.IsNullOrEmpty(timelineTransactionRequest.EventCode))
            {
                filters.Add(filterQueryBuilder.Where(o => o.EventId == timelineTransactionRequest.EventCode));
            }
            if (!String.IsNullOrEmpty(timelineTransactionRequest.ChildEventCode))
            {
                filters.Add(filterQueryBuilder.Where(o => o.ChildEventCode == timelineTransactionRequest.ChildEventCode));
            }
            if (!String.IsNullOrEmpty(timelineTransactionRequest.Type))
            {
                filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.Type == timelineTransactionRequest.Type));
            }
            if (timelineTransactionRequest.StartDate != null)
            {
                filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime >= (DateTime)timelineTransactionRequest.StartDate));
            }
            if (timelineTransactionRequest.EndDate != null)
            {
                filters.Add(filterQueryBuilder.Where(o => o.TransactionDetail.DateTime <= (DateTime)timelineTransactionRequest.EndDate));
            }
            return GetCombinedFilterWithAnd(filters);
        }
        public static FilterDefinition<RecurrenceModel.RecurrenceReward> PrepareFilterForRecurrenceRewardQuery(this RecurrenceModel.RecurrenceRewardQueueRequest recurrenceRewardQueueRequest)
        {
            List<FilterDefinition<RecurrenceModel.RecurrenceReward>> filters = new List<FilterDefinition<RecurrenceModel.RecurrenceReward>>();
            FilterDefinitionBuilder<RecurrenceModel.RecurrenceReward> filterQueryBuilder = Builders<RecurrenceModel.RecurrenceReward>.Filter;
            if (recurrenceRewardQueueRequest.Referrer != null)
            {
                var referrer = recurrenceRewardQueueRequest.Referrer;
                if (!String.IsNullOrEmpty(referrer.Lob))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referrer.Lob == referrer.Lob));
                }
                if (!String.IsNullOrEmpty(referrer.CustomerId))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referrer.CustomerId == referrer.CustomerId));
                }
            }
            if (recurrenceRewardQueueRequest.Referee != null)
            {
                var referee = recurrenceRewardQueueRequest.Referee;
                if (!String.IsNullOrEmpty(referee.Lob))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referee.Lob == referee.Lob));
                }
                if (!String.IsNullOrEmpty(referee.CustomerId))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referee.CustomerId == referee.CustomerId));
                }
                if (!String.IsNullOrEmpty(referee.MobileNumber))
                {
                    filters.Add(filterQueryBuilder.Where(o => o.Referee.MobileNumber == referee.MobileNumber));
                }
            }           
            return GetCombinedFilterWithAnd<RecurrenceModel.RecurrenceReward>(filters);
        }
    }
}
