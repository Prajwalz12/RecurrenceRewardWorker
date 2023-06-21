using Domain.Builders;
using Domain.Models;
using Domain.Models.EnumMaster;
using Domain.Services;
using MongoDB.Driver;
using Newtonsoft.Json;
using CampaignModel = Domain.Models.CampaignModel;
using CustomerModel = Domain.Models.CustomerModel;
using CustomerTimelineEventMasterModel = Domain.Models.CustomerTimelineEventMasterModel;
using CustomerTimelineModel = Domain.Models.CustomerTimelineModel;
using GlobalConfigurationModel = Domain.Models.GlobalConfigurationModel;
using ReferralModel = Domain.Models.ReferralModel;
using ReferralRuleModel = Domain.Models.ReferralRuleModel;
using RewardModel = Domain.Models.RewardModel;
using TransactionModel = Domain.Models.TransactionModel;
using RecurrenceModel = Domain.Models.RecurrenceModel;

namespace Domain.Processors;

public class ProcessorService
{
    private readonly ILogger<ProcessorService> _logger;
    private readonly ICustomerEventService _customerEventService;
    private readonly ICampaignService _campaignService;
    private readonly ILoyaltyFraudManagementService _loyaltyFraudManagementService;
    private readonly IOfferMapService _offerMapService;
    private readonly ICumulativeTransactionService _cumulativeTransactionService;
    private readonly ICustomerSummaryService _customerSummaryService;
    private readonly WebUIDatabaseService _webUIDatabaseService;
    private readonly DBService.DBServiceClient _dBServiceClient;
    private readonly ITransactionRewardService _transactionRewardService;
    private readonly MongoService.MongoServiceClient _mongoServiceClient;
    private readonly IGroupCampaignTransactionService _groupCampaignTransactionService;
    private readonly IMissedTransactionService _missedTransactionService;

    private readonly ITransactionService _transactionService;
    private readonly ICustomerTimelineService _customerTimelineService;
    private readonly IReferralRuleService _referralRuleService;
    private readonly IGlobalConfigurationService _globalConfigurationService;
    private readonly ICustomerService _customerService;
    private readonly IRecurrenceRewardService _recurrenceRewardService;

    private readonly LPassOLTPDatabaseService _lPassOLTPDatabaseService;

    public ProcessorService
    (
        ILogger<ProcessorService> logger,
        ICampaignService campaignService,
        ILoyaltyFraudManagementService loyaltyFraudManagementService,
        ICumulativeTransactionService cumulativeTransactionService,
        ICustomerEventService customerEventService,
        IOfferMapService offerMapService,
        ICustomerSummaryService customerSummaryService,
        WebUIDatabaseService webUIDatabaseService,
        DBService.DBServiceClient dBServiceClient,
        ITransactionRewardService transactionRewardService,
        MongoService.MongoServiceClient mongoServiceClient,
        ITransactionService transactionService,
        IGroupCampaignTransactionService groupCampaignTransactionService,
        IMissedTransactionService missedTransactionService,
        ICustomerTimelineService customerTimelineService,
        IReferralRuleService referralRuleService,
        IGlobalConfigurationService globalConfigurationService,
        LPassOLTPDatabaseService lPassOLTPDatabaseService,
        ICustomerService customerService,
        IRecurrenceRewardService recurrenceRewardService
    )
    {
        _logger = logger;
        _campaignService = campaignService;
        _loyaltyFraudManagementService = loyaltyFraudManagementService;
        _cumulativeTransactionService = cumulativeTransactionService;
        _customerEventService = customerEventService;
        _offerMapService = offerMapService;
        _customerSummaryService = customerSummaryService;
        _webUIDatabaseService = webUIDatabaseService;

        _transactionRewardService = transactionRewardService;
        _dBServiceClient = dBServiceClient;
        _mongoServiceClient = mongoServiceClient;
        _transactionService = transactionService;
        _groupCampaignTransactionService = groupCampaignTransactionService;
        _missedTransactionService = missedTransactionService;
        _customerTimelineService = customerTimelineService;
        _referralRuleService = referralRuleService;
        _globalConfigurationService = globalConfigurationService;
        _lPassOLTPDatabaseService = lPassOLTPDatabaseService;
        _customerService= customerService;
        _recurrenceRewardService= recurrenceRewardService;
    }

   
    public List<TransactionModel.Transaction> GetTransactions(string transactionId)
    {
        var filterDefinition = Builders<TransactionModel.Transaction>.Filter.Where(o => (o.TransactionId == transactionId));
        return _transactionService.Get(filterDefinition);
    }
    public async Task<RecurrenceModel.RecurrenceReward> GetRecurrenceRewardAsync(RecurrenceModel.RecurrenceRewardQueueRequest recurrenceRewardQueueRequest)
    {
        var filterDefinition = QueryBuilder.PrepareFilterForRecurrenceRewardQuery(recurrenceRewardQueueRequest);
        var rewardData = _recurrenceRewardService.Get(filterDefinition).FirstOrDefault();
        return await Task.FromResult(rewardData).ConfigureAwait(false);
    }
    public async Task<CampaignModel.EarnCampaign> GetCampaign(string campaignId)
    {
        var filterDefinition = Builders<CampaignModel.EarnCampaign>.Filter.Where(o => o.IIFLCampaignId == campaignId);
        var campaign = _campaignService.GetCampaignsUsingFilter(filterDefinition).FirstOrDefault();
        return await Task.FromResult(campaign).ConfigureAwait(false);
    }
    public async Task<CustomerModel.Customer> GetCustomerAsync(ReferralModel.Referee referee)
    {
        var filterDefinition = Builders<CustomerModel.Customer>.Filter.Where(o => o.CustomerId == referee.CustomerId && o.Lob == referee.Lob);
        var customer = _customerService.Get(filterDefinition).FirstOrDefault();
        return await Task.FromResult(customer).ConfigureAwait(false);
    }
    public async Task<CustomerModel.Customer> GetCustomerAsync(ReferralModel.Referrer referrer)
    {
        var filterDefinition = Builders<CustomerModel.Customer>.Filter.Where(o => o.CustomerId == referrer.CustomerId && o.Lob == referrer.Lob);
        var customer = _customerService.Get(filterDefinition).FirstOrDefault();
        return await Task.FromResult(customer).ConfigureAwait(false);
    }


    public TransactionModel.ProcessedTransaction InsertIntoLoyaltyFraudConfirmedLogs(TransactionModel.ProcessedTransaction transaction)
    {
        return _loyaltyFraudManagementService.Create(transaction);
    }
    public TransactionModel.ProcessedTransaction InsertIntoCumulativeTransactionLogs(TransactionModel.ProcessedTransaction transaction)
    {
        return _cumulativeTransactionService.Create(transaction);
    }
    public List<CampaignModel.EarnCampaign> GetCampaignsUsingFilter(FilterDefinition<CampaignModel.EarnCampaign> filter)
    {
        return _campaignService.GetCampaignsUsingFilter(filter);
    }
    public List<CustomerModel.CustomerEvent> GetCustomerEvents(FilterDefinition<CustomerModel.CustomerEvent> filter)
    {
        return _customerEventService.GetCustomerEvents(filter);
    }
    public List<RewardModel.TransactionReward> GetTransactionRewards(FilterDefinition<RewardModel.TransactionReward> filter)
    {
        return _transactionRewardService.Get(filter);
    }
    public long GetCustomerEventsCount(FilterDefinition<CustomerModel.CustomerEvent> filter)
    {
        return _customerEventService.GetCustomerEventsCount(filter);
    }
    public TransactionModel.ProcessedTransaction InsertIntoOfferMap(TransactionModel.ProcessedTransaction processedTransaction)
    {
        return _offerMapService.Create(processedTransaction);
    }

    public List<TransactionModel.Transaction> GetTransactions(string mobileNumber, CampaignModel.EarnCampaign campaign)
    {
        var filterDefinition = Builders<TransactionModel.Transaction>.Filter.Where(o => (o.MobileNumber == mobileNumber) && (o.TransactionDetail.DateTime >= campaign.StartDate) && (o.TransactionDetail.DateTime <= campaign.EndDate));
        return _transactionService.Get(filterDefinition);
    }

    public List<TransactionModel.Transaction> GetTransactions(FilterDefinition<TransactionModel.Transaction> filterDefinition)
    {
        return _transactionService.Get(filterDefinition);
    }
    public async Task<LapsePolicy> GetLapsePolicy(string code)
    {
        ////return _webUIDatabaseService.GetLapsePolicyByCode(code);
        var response = await _dBServiceClient.LapsePolicies_GetLapsePolicyAsync(new DBService.LapsePolicyRequest() { Code = code }).ConfigureAwait(false);
        if (response == null)
        {
            return null;
        }
        var lapsePolicy = response.Data;
        if (lapsePolicy == null)
        {
            return null;
        }
        return new LapsePolicy()
        {
            Id = lapsePolicy.Id,
            Code = lapsePolicy.Code,
            DurationType = lapsePolicy.DurationType,
            DurationValue = lapsePolicy.DurationValue,
            Name = lapsePolicy.Name
        };
        //return new LapsePolicy()
        //{
        //    DurationType = "Days",
        //    DurationValue = 180
        //};
    }
    public CustomerModel.CustomerSummary GetCustomerSummary(string mobileNumber)
    {
        return _customerSummaryService.GetByMobileNumber(mobileNumber);
    }
    public CustomerModel.CustomerSummary GetCustomerSummaryByCustomerUniqueId(string customerUniqueId)
    {
        return _customerSummaryService.GetByCustomerUniqueId(customerUniqueId);
    }
    public async Task<List<DBEnumValue>> GetEnumValues(string masterCode = null)
    {
        return await _webUIDatabaseService.GetDBEnumValuesAsync(masterCode).ConfigureAwait(false);
    }
    public async Task<MongoService.TransactionReward> CreateTransactionReward(MongoService.TransactionReward transactionReward)
    {
        return await _mongoServiceClient.TransactionRewards_PostAsync(transactionReward).ConfigureAwait(false);
    }

    public async Task<MongoService.CustomerEvent> CreateCustomerEventAsync(MongoService.CustomerEvent customerEvent)
    {
        return await _mongoServiceClient.CustomerEvents_PostAsync(customerEvent).ConfigureAwait(false);
    }
    public async Task<CustomerModel.CustomerEvent> CreateCustomerEventAsync(CustomerModel.CustomerEvent customerEvent)
    {
        return await Task.FromResult(_customerEventService.Create(customerEvent)).ConfigureAwait(false);
    }
    public async Task<List<MongoService.CustomerMobileCampaignMapping>> GetCustomerMobileCampaignMappings(MongoService.CustomerMobileCampaignMappingRequest customerMobileCampaignMappingRequest)
    {
        return (await _mongoServiceClient.CustomerMobileCampaignMappings_GetCustomerMobileCampaignMappingsAsync(customerMobileCampaignMappingRequest).ConfigureAwait(false)).ToList();
    }

    public async Task<MongoService.CustomerMobileCampaignMapping> CreateCustomerMobileCampaignMapping(MongoService.CustomerMobileCampaignMappingRequest customerMobileCampaignMappingRequest)
    {
        return await _mongoServiceClient.CustomerMobileCampaignMappings_InsertCustomerMobileCampaignMappingsAsync(customerMobileCampaignMappingRequest).ConfigureAwait(false);
    }

    public async Task<IEnumerable<MongoService.TripleRewardCustomerMerchantMapping>> GetTripleRewardMerchantMappings(MongoService.TripleRewardCustomerMerchantMapping tripleRewardMerchantMapping)
    {
        return await _mongoServiceClient.TripleRewardCustomerMerchantMappings_FetchTripleRewardCustomerMerchantMappingAsync(tripleRewardMerchantMapping).ConfigureAwait(false);

    }
    public async Task<GroupedCampaignTransaction> GroupedCampaignTransaction(GroupedCampaignTransaction groupedCampaignTransaction)
    {
        var createGroupedCampaignTransactionResponse = _groupCampaignTransactionService.Create(groupedCampaignTransaction);
        return await Task.FromResult(createGroupedCampaignTransactionResponse).ConfigureAwait(false);
    }

    public void UpdateMissedTransaction(TransactionModel.Transaction transaction, string transactionMobileNumber, string transactionInternalCustomerId, string transactionExternalCustomerId)
    {
        var filterDefinition = Builders<MissedTransaction>.Filter.Where(o => o.TransactionReferenceNumber == transaction.TransactionDetail.RefNumber && o.MobileNumber == transactionMobileNumber && o.ProcessedTransaction.TransactionRequest.InternalCustomerId == transactionInternalCustomerId && o.ProcessedTransaction.TransactionRequest.ExternalCustomerId == transactionExternalCustomerId);

        var updateDefinition = Builders<MissedTransaction>.Update
            .Set(o => o.IsQueued, true)
            .Set(o => o.IsReceivedOnQualificationService, true)
            .Set(k => k.QueuedDateTime, System.DateTime.Now)
            .Set(k => k.ReceivedOnQualificationServiceDateTime, System.DateTime.Now);

        _missedTransactionService.Update(filterDefinition, updateDefinition);
    }
    public List<ReferralModel.Referral> GetReferralDetail(ReferralModel.ReferralRequest referralRequest)
    {
        return _lPassOLTPDatabaseService.GetReferrals(referralRequest);
    }
    public async Task<List<ProductMaster>> GetProductMasters(string productCode = null)
    {
        return await _webUIDatabaseService.GetProductMasters(productCode).ConfigureAwait(false);
    }
    public bool IsProductCodeExist(string productCode = null)
    {
        var productMasters = GetProductMasters(productCode).Result;
        if (productMasters != null && productMasters.Any())
        {
            return true;
        }
        return false;
    }
    public async Task<CustomerTimelineModel.CustomerTimeline> GetRefereeTimelineAsync(CustomerTimelineModel.CustomerTimelineRequest customerTimelineRequest)
    {
        var filterDefinition = QueryBuilder.PrepareFilterForCustomerTimelineQuery(customerTimelineRequest);
        return (await Task.FromResult(_customerTimelineService.Get(filterDefinition)).ConfigureAwait(false)).FirstOrDefault();
    }
    public async Task<List<CustomerTimelineModel.CustomerTimeline>> GetRefereeTimelinesAsync(CustomerTimelineModel.CustomerTimelineRequest customerTimelineRequest)
    {
        var filterDefinition = QueryBuilder.PrepareFilterForCustomerTimelineQuery(customerTimelineRequest);
        var customerTimeLines = _customerTimelineService.Get(filterDefinition);
        _logger.LogInformation("GetRefereeTimelinesAsync => CustomerTimeLines => {0}", JsonConvert.SerializeObject(customerTimeLines));
        return await Task.FromResult(customerTimeLines).ConfigureAwait(false);
    }
    public async Task<List<ReferralRuleModel.ReferralRule>> GetReferralRulesAsync(ReferralRuleModel.ReferralRuleRequest referralRuleRequest)
    {
        var filterDefinition = QueryBuilder.PrepareFilterForReferralRuleQuery(referralRuleRequest);
        return await Task.FromResult(_referralRuleService.Get(filterDefinition)).ConfigureAwait(false);
    }
    public async Task<CustomerTimelineEventMasterModel.CustomerTimelineEventMaster> GetCustomerTimelineEventMasterAsync(CustomerTimelineEventMasterModel.CustomerTimelineEventRequest customerTimelineEventRequest)
    {
        return await _webUIDatabaseService.GetCustomerTimelineEventMaster(customerTimelineEventRequest).ConfigureAwait(false);
    }        
    public async Task<CustomerTimelineModel.CustomerTimeline> CreateCustomerTimelineAsync(CustomerTimelineModel.CustomerTimeline customerTimeline)
    {
        return await Task.FromResult(_customerTimelineService.Create(customerTimeline)).ConfigureAwait(false);
    }
    public async Task<UpdateResult> UpdateCustomerTimelineAsync(CustomerTimelineModel.CustomerTimelineRequest customerTimelineRequest, ReferralModel.Referral activeReferral = null, CustomerTimelineModel.CustomerTimeline customerTimeline = null)
    {
        var filterDefinition = QueryBuilder.PrepareFilterForCustomerTimelineQuery(customerTimelineRequest);
        if (customerTimeline == null)
        {
            var updateDefinition = Builders<CustomerTimelineModel.CustomerTimeline>.Update.Set(o => o.EventDate, activeReferral.ReferredDate).Set(o => o.Status, true).Set(o=> o.ProcessedStatus, 1).Set(o => o.UpdatedDate, DateTime.Now).Inc(o=> o.EventOccuranceCount, 1);
            return await Task.FromResult(_customerTimelineService.Update(filterDefinition, updateDefinition)).ConfigureAwait(false);
        }
        var _updateDefinition = Builders<CustomerTimelineModel.CustomerTimeline>.Update.Set(o => o.EventDate, customerTimeline.EventDate).Set(o => o.Status, customerTimeline.Status).Set(o => o.ProcessedStatus, customerTimeline.ProcessedStatus).Set(o => o.UpdatedDate, DateTime.Now).Inc(o => o.EventOccuranceCount, 1);
        return await Task.FromResult(_customerTimelineService.Update(filterDefinition, _updateDefinition)).ConfigureAwait(false);
    }
    public async Task<UpdateResult> UpdateCustomerTimelineStatusAsync(CustomerTimelineModel.CustomerTimelineRequest customerTimelineRequest)
    {
        var filterDefinition = QueryBuilder.PrepareFilterForCustomerTimelineQuery(customerTimelineRequest);            
        var _updateDefinition = Builders<CustomerTimelineModel.CustomerTimeline>.Update.Set(o => o.ProcessedStatus, 1).Set(o => o.UpdatedDate, DateTime.Now).Inc(o => o.EventOccuranceCount, 1);
        return await Task.FromResult(_customerTimelineService.Update(filterDefinition, _updateDefinition)).ConfigureAwait(false);
    }
    public async Task<List<TransactionModel.Transaction>> GetTimelineTransactionsAsync(TransactionModel.TimelineTransactionRequest timelineTransactionRequest)
    {
        var filterDefinition = QueryBuilder.PrepareTransactionFilterForTimelineEventQuery(timelineTransactionRequest);
        return await Task.FromResult(_transactionService.Get(filterDefinition)).ConfigureAwait(false);
    }
    public async Task<bool> UpdateReferralAsync(ReferralModel.UpdateReferralRequest updateReferralRequest)
    {
        int updateReferralResponse = 0;
        try
        {
            updateReferralResponse = _lPassOLTPDatabaseService.UpdateReferral(updateReferralRequest);
        }
        catch (Exception ex)
        {
            var s = ex.ToString();
        }

        return await Task.FromResult(Convert.ToBoolean(updateReferralResponse)).ConfigureAwait(false);
    }
    //public async Task<MongoService.GlobalConfiguration>  GetGlobalConfigurationAsync()
    //{
    //    return (await _mongoServiceClient.GlobalConfiguration_GetGlobalConfigurationAsync().ConfigureAwait(false)).FirstOrDefault(o=> o.IsActive && o.IsPublished);
    //}
    public async Task<GlobalConfigurationModel.GlobalConfiguration> GetGlobalConfigurationAsync()
    {
        var globalConfiguration =  (_globalConfigurationService.Get()).FirstOrDefault();
        return await Task.FromResult(globalConfiguration).ConfigureAwait(false);
    }
    public async Task<List<TransactionModel.Transaction>> GetTransactionByCustomerIdAsync(string customerId)
    {
        var filterDefinition = Builders<TransactionModel.Transaction>.Filter.Where(o=> o.TransactionDetail.Customer.CustomerId == customerId);
        return await Task.FromResult(_transactionService.Get(filterDefinition)).ConfigureAwait(false);
    }
    
    public async Task<TransactionModel.Transaction> GetTransactionAsync(string transactionId)
    {
        var transaction = _transactionService.Get(transactionId);
        return await Task.FromResult(transaction).ConfigureAwait(false);
    }
    public async Task<CampaignModel.EarnCampaign> GetCampaignAsync(string campaignId)
    {
        var campaign = _campaignService.Get(campaignId);
        return await Task.FromResult(campaign).ConfigureAwait(false);
    }
}
