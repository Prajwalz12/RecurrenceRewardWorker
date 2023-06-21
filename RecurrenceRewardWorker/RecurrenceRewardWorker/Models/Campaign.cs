using Domain.Models.RecurrenceModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Domain.Models.CampaignModel
{

    #region Internal Classes
    public class CumulativeAndNonCumulativeCampaignResponse
    {
        public IEnumerable<CampaignModel.EarnCampaign> CumulativeCampaigns { get; set; }
        public IEnumerable<CampaignModel.EarnCampaign> NonCumulativeCampaigns { get; set; }
    }
    public class RejectedAndNonRejectedCampaignFilterResponse
    {
        public IEnumerable<CampaignModel.EarnCampaign> RejectedCampaigns { get; set; }
        public IEnumerable<CampaignModel.EarnCampaign> NonRejectedCampaigns { get; set; }
    }
    public class UnlockedCampaignCheckResponse
    {
        public bool IsUnlock { get; set; }
        public string IssuanceMode { get; set; }
        public string OfferType { get; set; }
    }
    public class UnlockEventDetailResponse
    {
        public string EventName { get; set; }
        public bool IsHybridUnlock { get; set; } = false;
        public CampaignModel.WalletLoad WalletLoad { get; set; } = null;
        public CampaignModel.WalletCreation WalletCreation { get; set; } = null;
        public CampaignModel.ClearBounceEMI ClearBounceEMI { get; set; } = null;
        public CampaignModel.CustomRewarded CustomRewarded { get; set; } = null;
        public CampaignModel.KYC KYC { get; set; } = null;
        public CampaignModel.Signup Signup { get; set; } = null;
        public CampaignModel.VPACreated VPACreated { get; set; } = null;
        public CampaignModel.MRNCreation MRNCreation { get; set; } = null;
        public CampaignModel.CDFinancing CDFinancing { get; set; } = null;
        //public CustomUpload CustomRewarding { get; set; }
    }
    public class DirectAndLockUnlockCampaignCheckResponse
    {
        public IEnumerable<CampaignModel.EarnCampaign> DirectCampaigns { get; set; }
        public IEnumerable<CampaignModel.EarnCampaign> LockUnlockCampaigns { get; set; }
    }
    #endregion


    #region Campaign Section

    #region Common
    [BsonIgnoreExtraElements]
    public class Document<T>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public T Id { get; set; }

        [BsonElement("createdBy")]
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createdAt")]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedBy")]
        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("updatedAt")]
        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("deletedBy")]
        [JsonProperty("deletedBy")]
        public string DeletedBy { get; set; }

        [BsonElement("isDeleted")]
        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [BsonElement("isPublished")]
        [JsonProperty("isPublished")]
        public bool IsPublished { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("publishedAt")]
        [JsonProperty("publishedAt")]
        public DateTime? PublishedAt { get; set; }

        [BsonElement("versionId")]
        [JsonProperty("versionId")]
        public string VersionId { get; set; }

        [BsonElement("approver")]
        [JsonProperty("approver")]
        public string Approver { get; set; }

        [BsonElement("status")]
        [JsonProperty("status")]
        public string Status { get; set; } // Active, 

        [BsonElement("reasonTitle")]
        [JsonProperty("reasonTitle")]
        public string ReasonTitle { get; set; }

        [BsonElement("reasonDescription")]
        [JsonProperty("reasonDescription")]
        public string ReasonDescription { get; set; }

        [BsonElement("isDeployed")]
        [JsonProperty("isDeployed")]
        public int IsDeployed { get; set; } = 0;

        [BsonElement("oncePerCampaign")]
        [JsonProperty("oncePerCampaign")]
        public bool OncePerCampaign { get; set; }

        [BsonElement("onceInLifeTime")]
        [JsonProperty("onceInLifeTime")]
        public OnceInLifeTime OnceInLifeTime { get; set; }

    }
    #endregion

    #region Campaign
    [BsonIgnoreExtraElements]
    public class EarnCampaign : Document<string>
    {

        [BsonElement("campaignType")]
        [JsonProperty("campaignType")]
        public string CampaignType { get; set; }

        //>>>>>>>>>>>>>>>>>>>..General Campaign>>>>>>>>>>>>>>>>>>>>>>       

        [BsonElement("campaignName")]
        [JsonProperty("campaignName")]
        public string CampaignName { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("startDate")]
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("endDate")]
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("unLockExpiryDate")]
        [JsonProperty("unLockExpiryDate")]
        public DateTime? UnLockExpiryDate { get; set; } = null;

        [BsonElement("iiflCampaignId")]
        [JsonProperty("iiflCampaignId")]
        public string IIFLCampaignId { get; set; }

        [BsonElement("offerType")]
        [JsonProperty("offerType")]
        public string OfferType { get; set; } //activity || payment || hybrid

        [BsonElement("lob")]
        [JsonProperty("lob")]
        public string LOB { get; set; }

        [BsonElement("customerType")]
        [JsonProperty("customerType")]
        public List<string> CustomerType { get; set; }

        [BsonElement("customerStatus")]
        [JsonProperty("customerStatus")]
        public List<string> CustomerStatus { get; set; }

        [BsonElement("rewardCategory")]
        [JsonProperty("rewardCategory")]
        public string RewardCategory { get; set; }

        //[BsonElement("channel")]
        //[JsonProperty("channel")]
        //public string Channel { get; set; }

        [BsonElement("channel")]
        [JsonProperty("channel")]
        public List<string> Channel { get; set; }

        [BsonElement("mode")]
        [JsonProperty("mode")]
        public List<string> Mode { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("applyProductCode")]
        [JsonProperty("applyProductCode")]
        public int ApplyProductCode { get; set; } = 0; // 0 = Yes, 1 = No, 2 = Any

        [BsonElement("productCode")]
        [JsonProperty("productCode")]
        public List<string> ProductCode { get; set; }

        [BsonElement("installationSource")]
        [JsonProperty("installationSource")]
        public InstallationSource InstallationSource { get; set; }

        [BsonElement("segment")]
        [JsonProperty("segment")]
        public List<string> Segment { get; set; }

        [BsonElement("dynamicSegment")]
        [JsonProperty("dynamicSegment")]
        public List<string> DynamicSegment { get; set; }

        [BsonElement("rmsAttributes")]
        [JsonProperty("rmsAttributes")]
        public List<RMSAttribute> RmsAttributes { get; set; }

        [BsonElement("filter")]
        [JsonProperty("filter")]
        public Filter Filter { get; set; }

        //>>>>>>>>>>>>>>>>>>>Reward Criteria Campaign>>>>>>>>>>>>>>>>>>>>>>
        [BsonElement("rewardCriteria")]
        [JsonProperty("rewardCriteria")]
        public RewardCriteria RewardCriteria { get; set; }


        //>>>>>>>>>>>>>>>>>>>Reward option>>>>>>>>>>>>>>>>>>>>>>

        [BsonElement("rewardOption")]
        [JsonProperty("rewardOption")]
        public List<RewardOption> RewardOption { get; set; }

        //>>>>>>>>>>>>>>>>>>>Content Tab>>>>>>>>>>>>>>>>>>>>>>

        [BsonElement("content")]
        [JsonProperty("content")]
        public Content Content { get; set; }

        //>>>>>>>>>>>>>>>>>>>Alert Tab>>>>>>>>>>>>>>>>>>>>>>
        [BsonElement("alert")]
        [JsonProperty("alert")]
        public Alert Alert { get; set; }
    }
    #endregion

    [BsonIgnoreExtraElements]
    public class OnceInLifeTime
    {
        [BsonElement("value")]
        [JsonProperty("value")]
        public bool Value { get; set; }

        [BsonElement("tags")]
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class InstallationSource
    {
        [BsonElement("source")]
        [JsonProperty("source")]
        public List<string> Source { get; set; }

        [BsonElement("anyInstallationSource")]
        [JsonProperty("anyInstallationSource")]
        public bool AnyInstallationSource { get; set; }
    }

    #region Filter Campaign
    [BsonIgnoreExtraElements]
    public class Filter
    {

        [BsonElement("eventCode")]
        [JsonProperty("eventCode")]
        public List<string> EventCode { get; set; } = new List<string>();

        [BsonElement("isDirect")]
        [JsonProperty("isDirect")]
        public bool IsDirect { get; set; } = false;

        [BsonElement("directEvent")]
        [JsonProperty("directEvent")]
        public string DirectEvent { get; set; } = String.Empty;

        [BsonElement("isActivityEventCount")]
        [JsonProperty("isActivityEventCount")]
        public bool IsActivityEventCount { get; set; }

        [BsonElement("isReferralProgram")]
        [JsonProperty("isReferralProgram")]
        public bool IsReferralProgram { get; set; }

        [BsonElement("isAdditionalCondition")]
        [JsonProperty("isAdditionalCondition")]
        public bool IsAdditionalCondition { get; set; }

        [BsonElement("isLock")]
        [JsonProperty("isLock")]
        public bool IsLock { get; set; } = false;

        [BsonElement("lockEvent")]
        [JsonProperty("lockEvent")]
        public string LockEvent { get; set; } = String.Empty;

        [BsonElement("isUnlock")]
        [JsonProperty("isUnlock")]
        public bool IsUnlock { get; set; } = false;

        [BsonElement("unlockEvent")]
        [JsonProperty("unlockEvent")]
        public string UnlockEvent { get; set; } = String.Empty;

        [BsonElement("isTripleReward")]
        [JsonProperty("isTripleReward")]
        public bool IsTripleReward { get; set; } = false;

        [BsonElement("isCumulative")]
        [JsonProperty("isCumulative")]
        public bool IsCumulative { get; set; } = false;

        [BsonElement("isRecurrenceReward")]
        [JsonProperty("isRecurrenceReward")]
        public bool IsRecurrenceReward { get; set; } = false;
    }
    #endregion

    #region RMS
    [BsonIgnoreExtraElements]
    public class RMSAttribute
    {
        [BsonElement("attributeType")]
        [JsonProperty("attributeType")]
        public string AttributeType { get; set; } //points || cashback

        [BsonElement("parameter")]
        [JsonProperty("parameter")]
        public string Parameter { get; set; } //available balance || lifetime earn

        [BsonElement("parameterCode")]
        [JsonProperty("parameterCode")]
        public string parameterCode { get; set; }

        [BsonElement("startRange")]
        [JsonProperty("startRange")]
        public int StartRange { get; set; }

        [BsonElement("endRange")]
        [JsonProperty("endRange")]
        public int EndRange { get; set; }
    }
    #endregion

    #region RewardCriteria origin

    #region RewardCriteria
    [BsonIgnoreExtraElements]
    public class RewardCriteria
    {
        [BsonElement("rewardIssuance")]
        [JsonProperty("rewardIssuance")]
        public string RewardIssuance { get; set; } //Direct, Withlockandunlock

        [BsonElement("direct")]
        [JsonProperty("direct")]
        public Direct Direct { get; set; }

        [BsonElement("withLockUnlock")]
        [JsonProperty("withLockUnlock")]
        public WithLockUnlock WithLockUnlock { get; set; }

        [BsonElement("asScratchCard")]
        [JsonProperty("asScratchCard")]
        public AsScratchCard AsScratchCard { get; set; }

        [BsonElement("referralProgram")]
        [JsonProperty("referralProgram")]
        public ReferralProgram ReferralProgram { get; set; }

        [BsonElement("isAdditionalCondition")]
        [JsonProperty("isAdditionalCondition")]
        public bool IsAdditionalCondition { get; set; }

        [BsonElement("additionalCondition")]
        [JsonProperty("additionalCondition")]
        public AdditionalCondition AdditionalCondition { get; set; }
    }

    public class ReferralProgram
    {
        [BsonElement("status")]
        [JsonProperty("status")]
        public bool Status { get; set; }

        [BsonElement("capping")]
        [JsonProperty("capping")]
        public string Capping { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("rewardNarration")]
        [JsonProperty("rewardNarration")]
        public string RewardNarration { get; set; }

        [BsonElement("associatedRule")]
        [JsonProperty("associatedRule")]
        public string AssociatedRule { get; set; }

        [BsonElement("rewardOptions")]
        [JsonProperty("rewardOptions")]
        public List<RewardOption> RewardOptions { get; set; }

        [BsonElement("isDynamicSegment")]
        [JsonProperty("isDynamicSegment")]
        public bool IsDynamicSegment { get; set; }

        [BsonElement("dynamicSegments")]
        [JsonProperty("dynamicSegments")]
        public List<DynamicSegment> DynamicSegments { get; set; }

        [BsonElement("isRecurrenceReward")]
        [JsonProperty("isRecurrenceReward")]
        public bool IsRecurrenceReward { get; set; }

        [BsonElement("recurrenceReward")]
        [JsonProperty("recurrenceReward")]
        public RecurrenceReward RecurrenceReward { get; set; }
    }
    #endregion

    public class DynamicSegment
    {
        [BsonElement("dynamicSegmentCode")]
        [JsonProperty("dynamicSegmentCode")]
        public string DynamicSegmentCode { get; set; }

        [BsonElement("associatedRule")]
        [JsonProperty("associatedRule")]
        public string AssociatedRule { get; set; }

        [BsonElement("rewardOptions")]
        [JsonProperty("rewardOptions")]
        public List<RewardOption> RewardOptions { get; set; }

    }

    #region Direct
    [BsonIgnoreExtraElements]
    public class Direct
    {
        //activity dirct
        [BsonIgnoreIfNull]
        [BsonElement("activityDirect")]
        [JsonProperty("activityDirect")]
        public ActivityDirect ActivityDirect { get; set; }

        //payment direct
        [BsonIgnoreIfNull]
        [BsonElement("paymentDirect")]
        [JsonProperty("paymentDirect")]
        public PaymentDirect PaymentDirect { get; set; }
    }
    #endregion

    #region AdditionalCondition
    public class AdditionalCondition
    {
        [BsonElement("referenceEvent")]
        [JsonProperty("referenceEvent")]
        public ReferenceEvent ReferenceEvent { get; set; }

        [BsonElement("additionalConditionDuration")]
        [JsonProperty("additionalConditionDuration")]
        public AdditionalConditionDuration AdditionalConditionDuration { get; set; }
    }
    #endregion

    #region ReferenceEvent
    public class ReferenceEvent
    {
        [BsonElement("code")]
        [JsonProperty("code")]
        public string Code { get; set; }

        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

    }
    #endregion

    #region AdditionalConditionDuration
    public class AdditionalConditionDuration
    {
        [BsonElement("valueType")]
        [JsonProperty("valueType")]
        public string ValueType { get; set; } = "Day";

        [BsonElement("value")]
        [JsonProperty("value")]
        public int Value { get; set; }
    }
    #endregion

    #region WithLockUnlock
    public class WithLockUnlock
    {
        //Activity withLockUnlock
        [BsonIgnoreIfNull]
        [BsonElement("activityWithLockUnlock")]
        [JsonProperty("activityWithLockUnlock")]
        public ActivityWithLockUnlock ActivityWithLockUnlock { get; set; }

        //hybrid withLockUnlock
        [BsonIgnoreIfNull]
        [BsonElement("hybridWithLockUnlock")]
        [JsonProperty("hybridWithLockUnlock")]
        public HybridWithLockUnlock HybridWithLockUnlock { get; set; }

    }
    #endregion

    #region AsScratchCard

    public class AsScratchCard
    {
        [BsonElement("transactionType")]
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }

        [BsonElement("duration")]
        [JsonProperty("duration")]
        public Duration Duration { get; set; }
    }
    #endregion

    #region Duration
    public class Duration
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [BsonElement("recurrence")]
        [JsonProperty("recurrence")]
        public string Recurrence { get; set; }

        [BsonElement("paymentCategories")]
        [JsonProperty("paymentCategories")]
        public List<string> PaymentCategories { get; set; }

        [BsonElement("paymentInstruments")]
        [JsonProperty("paymentInstruments")]
        public List<string> PaymentInstruments { get; set; }

        [BsonElement("isMinTransaction")]
        [JsonProperty("isMinTransaction")]
        public bool IsMinTransaction { get; set; }

        [BsonElement("minTransactionAmount")]
        [JsonProperty("minTransactionAmount")]
        public int MinTransactionAmount { get; set; }

        [BsonElement("transactionCount")]
        [JsonProperty("transactionCount")]
        public int TransactionCount { get; set; }

        [BsonElement("merchant")]
        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; }
    }
    #endregion

    #region Merchant Triple Reward
    public class Merchant
    {

        [BsonElement("merchantCategory")]
        [JsonProperty("merchantCategory")]
        public List<string> MerchantCategory { get; set; }

        [BsonElement("groupMerchantId")]
        [JsonProperty("groupMerchantId")]
        public List<string> GroupMerchantId { get; set; }

        [BsonElement("merchantId")]
        [JsonProperty("merchantId")]
        public List<string> MerchantId { get; set; }

        [BsonElement("merchantSegment")]
        [JsonProperty("merchantSegment")]
        public string MerchantSegment { get; set; }

        [BsonElement("mode")]
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [BsonElement("segment")]
        [JsonProperty("segment")]
        public MerchantSegment Segment { get; set; }
    }
    #endregion

    #region Segment Triple Reward
    public class MerchantSegment
    {
        [BsonElement("segmentCode")]
        [JsonProperty("segmentCode")]
        public List<string> SegmentCode { get; set; }

        [BsonElement("isIncluded")]
        [JsonProperty("isIncluded")]
        public bool IsIncluded { get; set; }
    }
    #endregion

    #region ActiveDirect
    public class ActivityDirect
    {
        [BsonElement("event")]
        [JsonProperty("event")]
        public ActivityEvent Event { get; set; }

        [BsonElement("isActivityEventCount")]
        [JsonProperty("isActivityEventCount")]
        public bool IsActivityEventCount { get; set; }

        [BsonElement("activityEventCount")]
        [JsonProperty("activityEventCount")]
        [BsonIgnoreIfNull]
        public ActivityEventCount ActivityEventCount { get; set; }
    }
    #endregion

    #region ActivityEventCount
    public class ActivityEventCount
    {
        [BsonElement("value")]
        [JsonProperty("value")]
        public int Value { get; set; }
    }
    #endregion

    #region ActiveWithLockUnlock
    public class ActivityWithLockUnlock
    {
        [BsonElement("isLockExpire")]
        [JsonProperty("isLockExpire")]
        public bool IsLockExpire { get; set; }

        [BsonElement("noOfDays")]
        [JsonProperty("noOfDays")]
        public int NoOfDays { get; set; } //if isLockExpire

        [BsonElement("lockEvent")]
        [JsonProperty("lockEvent")]
        public ActivityEvent LockEvent { get; set; }

        [BsonElement("unLockEvent")]
        [JsonProperty("unLockEvent")]
        public ActivityEvent UnLockEvent { get; set; }
    }
    #endregion

    #region PaymentDirect
    public class PaymentDirect
    {

        [BsonElement("transactionType")]
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }    //single,cumulative and any

        [BsonElement("single")]
        [JsonProperty("single")]
        public Single Single { get; set; }

        [BsonElement("cumulative")]
        [JsonProperty("cumulative")]
        public Cumulative Cumulative { get; set; }


        //[BsonElement("paymentCategories")]
        //[JsonProperty("paymentCategories")]
        //public List<string> PaymentCategories { get; set; }

        [BsonElement("paymentCategories")]
        [JsonProperty("paymentCategories")]
        public string PaymentCategories { get; set; }

        [BsonElement("paymentInstruments")]
        [JsonProperty("paymentInstruments")]
        public List<string> PaymentInstruments { get; set; }

        [BsonElement("merchant")]
        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; }


        [BsonElement("bbps")]
        [JsonProperty("bbps")]
        public BBPS BBPS { get; set; }

        [BsonElement("loanDisbursal")]
        [JsonProperty("loanDisbursal")]
        public LoanDisbursal LoanDisbursal { get; set; }

        [BsonElement("emiRepayment")]
        [JsonProperty("emiRepayment")]
        public EMIRepayment EMIRepayment { get; set; }

        [BsonElement("trading")]
        [JsonProperty("trading")]
        public Trading Trading { get; set; }

        [BsonElement("subscription")]
        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }

        [BsonElement("fundTransfer")]
        [JsonProperty("fundTransfer")]
        public FundTransfer FundTransfer { get; set; }

        [BsonElement("investment")]
        [JsonProperty("investment")]
        public Investment Investment { get; set; }

        [BsonElement("premium")]
        [JsonProperty("premium")]
        public Premium Premium { get; set; }
    }

    public class Premium
    {
        [BsonElement("premiumType")]
        [JsonProperty("premiumType")]
        public List<string> PremiumType { get; set; }
    }
    public class Subscription
    {
        [BsonElement("subscriptionType")]
        [JsonProperty("subscriptionType")]
        public List<string> SubscriptionType { get; set; }
        [BsonElement("investorType")]
        [JsonProperty("investorType")]
        public List<string> InvestorType { get; set; }
    }

    public class FundTransfer
    {
        [BsonElement("transferType")]
        [JsonProperty("transferType")]
        public List<string> TransferType { get; set; }
    }
    public class Investment
    {
        [BsonElement("sipType")]
        [JsonProperty("sipType")]
        public List<string> SIPType { get; set; }

        [BsonElement("investmentType")]
        [JsonProperty("investmentType")]
        public List<string> InvestmentType { get; set; }
    }
    public class EMIRepayment
    {
        [BsonElement("emiType")]
        [JsonProperty("emiType")]
        public List<string> EMIType { get; set; }
    }

    public class Trading
    {
        [BsonElement("tradingType")]
        [JsonProperty("tradingType")]
        public List<string> TradingType { get; set; }
    }

    public class LoanDisbursal
    {
        [BsonElement("disbursalType")]
        [JsonProperty("disbursalType")]
        public List<string> DisbursalType { get; set; }

        [BsonElement("loanType")]
        [JsonProperty("loanType")]
        public List<string> LoanType { get; set; }

    }

    #endregion

    #region BBPS
    public class BBPS
    {
        [BsonElement("anyCategory")]
        [JsonProperty("anyCategory")]
        public bool AnyCategory { get; set; }

        [BsonElement("billerCategories")]
        [JsonProperty("billerCategories")]
        public List<BBPSBillerCategory> BillerCategories { get; set; }
    }
    #endregion

    #region
    public class BBPSBillerCategory
    {
        [BsonElement("biller")]
        [JsonProperty("biller")]
        public string Biller { get; set; }

        [BsonElement("billerCategory")]
        [JsonProperty("billerCategory")]
        public string BillerCategory { get; set; }

    }
    #endregion

    #region HybridWithLockUnlock
    public class HybridWithLockUnlock
    {
        [BsonElement("isLockExpire")]
        [JsonProperty("isLockExpire")]
        public bool IsLockExpire { get; set; }

        [BsonElement("noOfDays")]
        [JsonProperty("noOfDays")]
        public int NoOfDays { get; set; }

        //lock events 
        [BsonElement("lockActivity")]
        [JsonProperty("lockActivity")]
        public ActivityEvent LockActivity { get; set; }

        //unlock events
        //hybrid payment
        [BsonIgnoreIfNull]
        [BsonElement("unlockPayment")]
        [JsonProperty("unlockPayment")]
        public PaymentDirect UnlockPayment { get; set; }

        [BsonElement("transactionType")]
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }    //single,cumulative and any

        [BsonElement("single")]
        [JsonProperty("single")]
        public Single Single { get; set; }

        [BsonElement("cumulative")]
        [JsonProperty("cumulative")]
        public Cumulative Cumulative { get; set; }


        [BsonElement("paymentCategories")]
        [JsonProperty("paymentCategories")]
        public List<string> PaymentCategories { get; set; }

        [BsonElement("paymentInstruments")]
        [JsonProperty("paymentInstruments")]
        public List<string> PaymentInstruments { get; set; }

        [BsonElement("merchant")]
        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; }


        [BsonElement("bbps")]
        [JsonProperty("bbps")]
        public BBPS BBPS { get; set; }

    }
    #endregion

    #region ActivityEvent
    [BsonIgnoreExtraElements]
    public class ActivityEvent
    {
        [BsonElement("eventName")]
        [JsonProperty("eventName")]
        public string EventName { get; set; }
        [BsonElement("any")]
        [JsonProperty("any")]
        [BsonIgnoreIfNull]
        public Any Any { get; set; }

        [BsonElement("clearBounceEMI")]
        [JsonProperty("clearBounceEMI")]
        [BsonIgnoreIfNull]

        public ClearBounceEMI ClearBounceEMI { get; set; }
        [BsonElement("customRewarded")]
        [JsonProperty("customRewarded")]
        [BsonIgnoreIfNull]

        public CustomRewarded CustomRewarded { get; set; }
        [BsonElement("kyc")]
        [JsonProperty("kyc")]
        [BsonIgnoreIfNull]

        public KYC KYC { get; set; }
        [BsonElement("signup")]
        [JsonProperty("signup")]
        [BsonIgnoreIfNull]

        public Signup Signup { get; set; }
        [BsonElement("vpaCreated")]
        [JsonProperty("vpaCreated")]
        [BsonIgnoreIfNull]

        public VPACreated VPACreated { get; set; }
        [BsonElement("walletCreation")]
        [JsonProperty("walletCreation")]
        [BsonIgnoreIfNull]

        public WalletCreation WalletCreation { get; set; }

        [BsonElement("walletLoad")]
        [JsonProperty("walletLoad")]
        [BsonIgnoreIfNull]
        public WalletLoad WalletLoad { get; set; }

        [BsonElement("mrnCreation")]
        [JsonProperty("mrnCreation")]
        [BsonIgnoreIfNull]
        public MRNCreation MRNCreation { get; set; }

        [BsonElement("cdFinancing")]
        [JsonProperty("cdFinancing")]
        [BsonIgnoreIfNull]
        public CDFinancing CDFinancing { get; set; }

        [BsonElement("customUpload")]
        [JsonProperty("customUpload")]
        [BsonIgnoreIfNull]
        public CustomUpload CustomUpload { get; set; }

        [BsonElement("emiRepayment")]
        [JsonProperty("emiRepayment")]
        [BsonIgnoreIfNull]
        public EMIRepayment EMIRepayment { get; set; }

        [BsonElement("doIssue")]
        [JsonProperty("doIssue")]
        [BsonIgnoreIfNull]
        public DOIssue DOIssue { get; set; }

        [BsonElement("diCompleted")]
        [JsonProperty("diCompleted")]
        [BsonIgnoreIfNull]
        public DICompleted DICompleted { get; set; }

        [BsonElement("genericActivity")]
        [JsonProperty("genericActivity")]
        [BsonIgnoreIfNull]
        public GenericActivity GenericActivity { get; set; }

    }
    #endregion

    #region Events
    public class CustomUpload { }

    public class CDFinancing { }

    public class MRNCreation { }

    public class Signup
    {
        //    public string EventName { get; set; } = "SignedUp";
        //    [BsonElement("signupType")]
        //    [JsonProperty("signupType")]
        //    public string SignupType { get; set; }    //organic || targeted
    }
    public class ClearBounceEMI
    {
        [BsonElement("bounceEMIPaid")]
        [JsonProperty("bounceEMIPaid")]
        public bool BounceEMIPaid { get; set; }

        [BsonElement("emiAmount")]
        [JsonProperty("emiAmount")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EMIAmount { get; set; }
    }
    public class CustomRewarded
    {
        [BsonElement("file")]
        [JsonProperty("file")]
        public byte[] File { get; set; }

        [BsonElement("fileName")]
        [JsonProperty("fileName")]
        public string FileName { get; set; }
    }
    public class KYC
    {
        [BsonElement("completionType")]
        [JsonProperty("completionType")]
        public string CompletionType { get; set; }
    }
    //public class SignedUp
    //{
    //    [BsonElement("signupType")]
    //    [JsonProperty("signupType")]
    //    public string SignupType { get; set; } //organic || targeted
    //}
    public class VPACreated
    {
        [BsonElement("vpa")]
        [JsonProperty("vpa")]
        public string VPA { get; set; }
    }
    public class WalletCreation
    {

    }
    public class WalletLoad
    {
        [BsonElement("loadCount")]
        [JsonProperty("loadCount")]
        public int LoadCount { get; set; }

        [BsonElement("paymentInstruments")]
        [JsonProperty("paymentInstruments")]
        public List<string> PaymentInstruments { get; set; }

        [BsonElement("isMinimumLoadAmount")]
        [JsonProperty("isMinimumLoadAmount")]
        public bool IsMinimumLoadAmount { get; set; }

        [BsonElement("minimumLoadAmount")]
        [JsonProperty("minimumLoadAmount")]
        public decimal MinimumLoadAmount { get; set; } //if IsMinimumLoadAmount=true

        [BsonElement("isLoadInterval")]
        [JsonProperty("isLoadInterval")]
        public bool IsLoadInterval { get; set; }

        [BsonElement("loadIntervalHours")]
        [JsonProperty("loadIntervalHours")]
        public int LoadIntervalHours { get; set; } //if LoadInterval=true

        [BsonElement("loadIntervalMinutes")]
        [JsonProperty("loadIntervalMinutes")]
        public int LoadIntervalMinutes { get; set; } //if LoadInterval=true
    }

    public class DOIssue { }
    public class DICompleted { }
    //public class EMIRepayment { }
    public class GenericActivity
    {
        [BsonElement("childEvent")]
        [JsonProperty("childEvent")]
        public string ChildEventCode { get; set; }
    }
    public class Any
    {

    }
    #endregion

    #region Transaction single and cumulative
    public class Single
    {

        [BsonElement("isTransactionCount")]
        [JsonProperty("isTransactionCount")]
        public bool IsTransactionCount { get; set; }

        [BsonElement("transactionCount")]
        [JsonProperty("transactionCount")]
        public int TransactionCount { get; set; }

        [BsonElement("isTransactionAmount")]
        [JsonProperty("isTransactionAmount")]
        public bool IsTransactionAmount { get; set; }

        //[BsonElement("minTransactionAmount")]
        //[JsonProperty("minTransactionAmount")]
        //public decimal? MinTransactionAmount { get; set; }

        [BsonElement("amount")]
        [JsonProperty("amount")]
        public Amount Amount { get; set; }

    }

    public class Amount
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [BsonElement("minAmount")]
        [JsonProperty("minAmount")]
        public MinAmount MinAmount { get; set; }

        [BsonElement("range")]
        [JsonProperty("range")]
        public Range Range { get; set; }
    }

    public class MinAmount
    {
        [BsonElement("value")]
        [JsonProperty("value")]
        public decimal? Value { get; set; }
    }

    public class Range
    {
        [BsonElement("minValue")]
        [JsonProperty("minValue")]
        public decimal? MinValue { get; set; }

        [BsonElement("maxValue")]
        [JsonProperty("maxValue")]
        public decimal? MaxValue { get; set; }
    }

    public class SpendOccurence
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; } //single || multiple

        [BsonElement("single")]
        [JsonProperty("single")]
        public SingleSpendOccurence Single { get; set; }

        [BsonElement("multiple")]
        [JsonProperty("multiple")]
        public MultipleSpendOccurance Multiple { get; set; }
    }
    public class SingleSpendOccurence
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; } = "Single";

        [BsonElement("recurrence")]
        [JsonProperty("recurrence")]
        public string Recurrence { get; set; }
    }
    public class MultipleSpendOccurance
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; } = "Multiple";

        [BsonElement("occurence")]
        [JsonProperty("occurence")]
        public int Occurence { get; set; }

        [BsonElement("recurrence")]
        [JsonProperty("recurrence")]
        public string Recurrence { get; set; }

        [BsonElement("spendType")]
        [JsonProperty("spendType")]
        public string SpendType { get; set; }
    }
    public class SpendBy
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; } //fixed || top spender

        [BsonElement("fixedSpendBy")]
        [JsonProperty("fixedSpendBy")]
        public FixedSpendBy FixedSpendBy { get; set; }

        [BsonElement("topSpenderSpendBy")]
        [JsonProperty("topSpenderSpendBy")]
        public TopSpenderSpendBy TopSpenderSpendBy { get; set; }

    }
    public class FixedSpendBy
    {

        [BsonElement("amount")]
        [JsonProperty("amount")]
        public decimal Amount { get; set; } //if isMinimumCumulativeAmount
    }
    public class TopSpenderSpendBy
    {

        [BsonElement("topSpenderType")]
        [JsonProperty("topSpenderType")]
        public string TopSpenderType { get; set; }

        [BsonElement("spendOccurence")]
        [JsonProperty("spendOccurence")]
        public SpendOccurence SpendOccurence { get; set; }
    }
    public class CumulativeBy
    {
        [BsonElement("cumulativeType")]
        [JsonProperty("cumulativeType")]
        public string CumulativeType { get; set; }

        [BsonElement("amount")]
        [JsonProperty("amount")]
        public AmountCumulative Amount { get; set; }

        [BsonElement("count")]
        [JsonProperty("count")]
        public CountCumulative Count { get; set; }
    }
    public class Cumulative
    {
        [BsonElement("cumulativeDuration")]
        [JsonProperty("cumulativeDuration")]
        public string CumulativeDuration { get; set; }

        [BsonElement("recurrenceType")]
        [JsonProperty("recurrenceType")]
        public string RecurrenceType { get; set; } // 1. daily, 2. weekly, 3. monthly

        [BsonElement("cumulativeBy")]
        [JsonProperty("cumulativeBy")]
        public CumulativeBy CumulativeBy { get; set; }

        [BsonElement("offerPeriodType")]
        [JsonProperty("offerPeriodType")]
        public string OfferPeriodType { get; set; } // New field added.
    }
    public class AmountCumulative
    {

        [BsonElement("spendBy")]
        [JsonProperty("spendBy")]
        public SpendBy SpendBy { get; set; }
    }
    public class CountCumulative
    {

        [BsonElement("count")]
        [JsonProperty("count")]
        public int Count { get; set; }

        [BsonElement("mode")]
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [BsonElement("isMinimumAmount")]
        [JsonProperty("isMinimumAmount")]
        public bool IsMinimumAmount { get; set; }

        [BsonElement("amount")]
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
    #endregion



    #endregion   

    public class RecurrenceReward
    {
        [BsonElement("isDynamicSegment")]
        [JsonProperty("isDynamicSegment")]
        public bool IsDynamicSegment { get; set; }

        [BsonElement("dynamicSegments")]
        [JsonProperty("dynamicSegments")]
        public List<RecurringDynamicSegment> DynamicSegments { get; set; }
    }

    public class RecurringDynamicSegment
    {
        [BsonElement("dynamicSegmentCode")]
        [JsonProperty("dynamicSegmentCode")]
        public string DynamicSegmentCode { get; set; }

        [BsonElement("associatedRule")]
        [JsonProperty("associatedRule")]
        public string AssociatedRule { get; set; }

        [BsonElement("rewardOptions")]
        [JsonProperty("rewardOptions")]
        public List<RewardOption> RewardOptions { get; set; }

        [BsonElement("additionalCondition")]
        [JsonProperty("additionalCondition")]
        public RecurrenceAdditionalCondition AdditionalCondition { get; set; }
    }

    public class RecurrenceAdditionalCondition
    {
        [BsonElement("referenceEvent")]
        [JsonProperty("referenceEvent")]
        public ReferenceEvent ReferenceEvent { get; set; }

        [BsonElement("additionalConditionDuration")]
        [JsonProperty("additionalConditionDuration")]
        public AdditionalConditionDuration AdditionalConditionDuration { get; set; }

        [BsonElement("isRewardingApplyOnlyOnBrokrageAmount")]
        [JsonProperty("isRewardingApplyOnlyOnBrokrageAmount")]
        public bool IsRewardingApplyOnlyOnBrokrageAmount { get; set; }
    }    


    #region Option

    public class RewardOption
    {
        [BsonElement("rewardType")]
        [JsonProperty("rewardType")]
        public string RewardType { get; set; } //point || cashback || voucher

        [BsonIgnoreIfNull]
        [BsonElement("points")]
        [JsonProperty("points")]
        public RewardTypePoints Points { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("cashback")]
        [JsonProperty("cashback")]
        public RewardTypeCashback Cashback { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("voucherDetails")]
        [JsonProperty("voucherDetails")]
        public RewardTypeVoucher VoucherDetails { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("promoVoucherDetails")]
        [JsonProperty("promoVoucherDetails")]
        public RewardTypePromoVoucher PromoVoucherDetails { get; set; }

    }
    public class RewardTypePromoVoucher
    {
        [BsonElement("voucherType")]
        [JsonProperty("voucherType")]
        public int VoucherType { get; set; } = 0;   //1 : Cashback, 2 : Inline discount

        [BsonElement("skuId")]
        [JsonProperty("skuId")]
        public int SkuId { get; set; } = 0;

        [BsonElement("valueType")]
        [JsonProperty("valueType")]
        public int ValueType { get; set; } = 0;     //1: Fixed, 2 : Percentage

        [BsonElement("fixValue")]
        [JsonProperty("fixValue")]
        public RewardValueTypeFixed FixValue { get; set; }

        [BsonElement("percentageValue")]
        [JsonProperty("percentageValue")]
        public RewardValueTypePercentage PercentageValue { get; set; }

        [BsonElement("validity")]
        [JsonProperty("validity")]
        public int Validity { get; set; } = 0;    //1: Dynamic, 2:Static

        [BsonElement("staticValidDate")]
        [JsonProperty("staticValidDate")]
        public string StaticValidDate { get; set; }

        [BsonElement("dynamicValidDay")]
        [JsonProperty("dynamicValidDay")]
        public int DynamicValidDay { get; set; } = 0;

        [BsonElement("quantity")]
        [JsonProperty("quantity")]
        public int Quantity { get; set; } = 0;
    }
    public class RewardTypePoints
    {
        [BsonElement("pointType")]
        [JsonProperty("pointType")]
        public string PointType { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("pointsFixed")]
        [JsonProperty("pointsFixed")]
        public RewardValueTypeFixed PointsFixed { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("pointsPercentage")]
        [JsonProperty("pointsPercentage")]
        public RewardValueTypePercentage PointsPercentage { get; set; }

        [BsonElement("expirePolicy")]
        [JsonProperty("expirePolicy")]
        public string ExpirePolicy { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("durationType")]
        [JsonProperty("durationType")]
        public string DurationType { get; set; } = "Days";

        [BsonIgnoreIfNull]
        [BsonElement("durationValue")]
        [JsonProperty("durationValue")]
        public int DurationValue { get; set; } = 0;

    }

    public class RewardTypeCashback
    {
        [BsonElement("cashBackType")]
        [JsonProperty("cashBackType")]
        public string CashBackType { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("cashBackFixed")]
        [JsonProperty("cashBackFixed")]
        public RewardValueTypeFixed CashBackFixed { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("cashBackPercentage")]
        [JsonProperty("cashBackPercentage")]
        public RewardValueTypePercentage CashBackPercentage { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("cashBackRandomized")]
        [JsonProperty("cashBackRandomized")]
        public RewardValueTypeRandomized CashBackRandomized { get; set; }
    }
    public class RewardTypeVoucher
    {

        [BsonElement("lob")]
        [JsonProperty("lob")]
        public string LOB { get; set; }

        [BsonElement("catalogueCode")]
        [JsonProperty("catalogueCode")]
        public List<string> CatalogueCode { get; set; }

        [BsonElement("categoryCode")]
        [JsonProperty("categoryCode")]
        public string CategoryCode { get; set; }

        [BsonElement("brandCode")]
        [JsonProperty("brandCode")]
        public string BrandCode { get; set; }

        [BsonElement("denomination")]
        [JsonProperty("denomination")]
        public string Denomination { get; set; }

        [BsonElement("validity")]
        [JsonProperty("validity")]
        public string Validity { get; set; }

        [BsonElement("quantity")]
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [BsonElement("sku")]
        [JsonProperty("sku")]
        public string SKU { get; set; }

        [BsonElement("voucherCost")]
        [JsonProperty("voucherCost")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal VoucherCost { get; set; }

        [BsonElement("isCheckedOTTMerchant")]
        [JsonProperty("isCheckedOTTMerchant")]
        public string IsCheckedOTTMerchant { get; set; } // OTT / Merchant
    }

    public class RewardValueTypeFixed
    {
        [BsonElement("value")]
        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
    public class RewardValueTypePercentage
    {
        [BsonElement("value")]
        [JsonProperty("value")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Value { get; set; }

        [BsonElement("maximumCashback")]
        [JsonProperty("maximumCashback")]
        public decimal MaximumCashback { get; set; }
    }
    public class RewardValueTypeRandomized
    {
        [BsonElement("budget")]
        [JsonProperty("budget")]
        public decimal Budget { get; set; }

        [BsonElement("median")]
        [JsonProperty("median")]
        public decimal Median { get; set; }

        [BsonElement("topEarnerAmount")]
        [JsonProperty("topEarnerAmount")]
        public decimal TopEarnerAmount { get; set; }

        [BsonElement("topTransactionPercentage")]
        [JsonProperty("topTransactionPercentage")]
        public decimal TopTransactionPercentage { get; set; }

        [BsonElement("bottomEarnerAmount")]
        [JsonProperty("bottomEarnerAmount")]
        public decimal BottomEarnerAmount { get; set; }

        [BsonElement("bottomTransactionPercentage")]
        [JsonProperty("bottomTransactionPercentage")]
        public decimal BottomTransactionPercentage { get; set; }

        [BsonElement("dailyUnusedBudget")]
        [JsonProperty("dailyUnusedBudget")]
        public string DailyUnusedBudget { get; set; } //lapse || add to pool

        [BsonElement("maxTxnPerCustomer")]
        [JsonProperty("maxTxnPerCustomer")]
        public int MaxTxnPerCustomer { get; set; }

        [BsonElement("assuredCashbak")]
        [JsonProperty("assuredCashbak")]
        public int AssuredCashback { get; set; }
    }
    #endregion


    #region Content
    [BsonIgnoreExtraElements]
    public class Content
    {
        [BsonElement("campaignDescription")]
        [JsonProperty("campaignDescription")]
        public string CampaignDescription { get; set; }

        [BsonElement("rewardNarration")]
        [JsonProperty("rewardNarration")]
        public string RewardNarration { get; set; }

        [BsonElement("cTAUrl")]
        [JsonProperty("cTAUrl")]
        public string CTAUrl { get; set; }

        [BsonElement("unlockCondition")]
        [JsonProperty("unlockCondition")]
        public string UnlockCondition { get; set; }

        [BsonElement("containerName")]
        [JsonProperty("containerName")]
        public string ContainerName { get; set; }


        [BsonElement("folderName")]
        [JsonProperty("folderName")]
        public string FolderName { get; set; }

        [BsonElement("termAndCondition")]
        [JsonProperty("termAndCondition")]
        public string TermAndCondition { get; set; }

        [BsonElement("images")]
        [JsonProperty("images")]
        public List<ECImage> Images { get; set; }

        [BsonElement("imageCount")]
        [JsonProperty("imageCount")]
        public int ImageCount { get; set; }
    }

    public class ECImage
    {
        [BsonElement("blobLocationUrl")]
        [JsonProperty("blobLocationUrl")]
        public string BlobLocationUrl { get; set; }

        [BsonElement("size")]
        [JsonProperty("size")]
        public string Size { get; set; }

        [BsonElement("actualFileName")]
        [JsonProperty("actualFileName")]
        public string ActualFileName { get; set; }

        [BsonElement("blioImageDetails")]
        [JsonProperty("blioImageDetails")]
        public string BlioImageDetails { get; set; }

        [BsonElement("uID")]
        [JsonProperty("uID")]
        public string UID { get; set; }

    }
    #endregion


    #region Alert
    public class Alert
    {
        [BsonElement("templateName")]
        [JsonProperty("templateName")]
        public string TemplateName { get; set; }

        [BsonElement("budget")]
        [JsonProperty("budget")]
        public Budget Budget { get; set; }
    }
    #endregion
    public class Budget
    {
        [BsonElement("range")]
        [JsonProperty("range")]
        public List<BudgetData> Range { get; set; }
    }

    public class BudgetData
    {
        [BsonElement("from")]
        [JsonProperty("from")]
        public int From { get; set; }

        [BsonElement("to")]
        [JsonProperty("to")]
        public int To { get; set; }

        [BsonElement("userID")]
        [JsonProperty("userID")]
        public string UserID { get; set; }

        [BsonElement("template")]
        [JsonProperty("template")]
        public Template Template { get; set; }
    }

    public class Template
    {
        [BsonElement("subject")]
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [BsonElement("body")]
        [JsonProperty("body")]
        public string Body { get; set; }
    }
    #endregion
}