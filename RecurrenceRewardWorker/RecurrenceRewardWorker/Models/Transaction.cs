using Domain.Models.CampaignModel;
using Domain.Models.Common.TransactionModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Domain.Models.TransactionModel
{
    public class ReferredTransaction
    {
        [BsonElement("transactionId")]
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [BsonElement("referee")]
        [JsonProperty("referee")]
        public Domain.Models.ReferralModel.Referee Referee { get; set; }

        [BsonElement("referrer")]
        [JsonProperty("referrer")]
        public Domain.Models.ReferralModel.Referrer Referrer { get; set; }
    }

    #region Common Transaction Section
    [BsonIgnoreExtraElements]
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("transactionId")]
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [BsonElement("mobileNumber")]
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [BsonElement("internalCustomerId")]
        [JsonProperty("internalCustomerId")]
        public string InternalCustomerId { get; set; }

        [BsonElement("externalCustomerId")]
        [JsonProperty("externalCustomerId")]
        public string ExternalCustomerId { get; set; }

        [BsonElement("lob")]
        [JsonProperty("lob")]
        public string LOB { get; set; }

        [BsonElement("eventId")]
        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [BsonElement("childEventCode")]
        [JsonProperty("childEventCode")]
        public string ChildEventCode { get; set; }

        [BsonElement("channelCode")]
        [JsonProperty("channelCode")]
        public string ChannelCode { get; set; }


        [BsonElement("customerDetail")]
        [JsonProperty("customerDetail")]
        public Common.TransactionModel.CustomerDetail CustomerDetail { get; set; }

        [BsonElement("wallet")]
        [JsonProperty("wallet")]
        public Common.TransactionModel.Wallet Wallet { get; set; }

        [BsonElement("campaign")]
        [JsonProperty("offer")]
        public Common.TransactionModel.Campaign Campaign { get; set; }

        [BsonElement("utm")]
        [JsonProperty("utm")]
        public Common.TransactionModel.UTM UTM { get; set; }

        [BsonElement("transactionDetail")]
        [JsonProperty("transactionDetail")]
        public Common.TransactionModel.TransactionDetail TransactionDetail { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createdDateTime")]
        [JsonProperty("createdDateTime")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [BsonElement("parentTransactionId")]
        [JsonProperty("parentTransactionId")]
        public string ParentTransactionId { get; set; } = null;

        [BsonElement("branchCode")]
        [JsonProperty("branchCode")]
        public string BranchCode { get; set; }

        [BsonElement("employeeCode")]
        [JsonProperty("employeeCode")]
        public string EmployeeCode { get; set; }

        [BsonElement("referral")]
        [JsonProperty("referral")]
        public Referral Referral { get; set; }

        //[BsonElement("type")]
        //[JsonProperty("type")]
        //public string Type { get; set; }

        [BsonElement("requestTraceId")]
        [JsonProperty("requestTraceId")]
        public string RequestTraceId { get; set; }

        [BsonElement("uniqueCustomerId")]
        [JsonProperty("uniqueCustomerId")]
        public string UniqueCustomerId { get; set; }

        //[BsonElement("ucIc")]
        //[JsonProperty("ucIc")]
        //public string UcIc { get; set; } //garbage not required


        [BsonElement("acquisitionPartnerCode")]
        [JsonProperty("acquisitionPartnerCode")]
        public string AcquisitionPartnerCode { get; set; }

        [BsonElement("investmentDetails")]
        [JsonProperty("investmentDetails")]
        public InvestmentDetails InvestmentDetails { get; set; }
    }
    #endregion

    #region Domain Transcction Section
    public class ManageCustomerReferralResponse
    {
        [BsonElement("processedTransaction")]
        [JsonProperty("processedTransaction")]
        public ProcessedTransaction ProcessedTransaction { get; set; } = new ProcessedTransaction();

        [BsonElement("isTimelineNeedProcessed")]
        [JsonProperty("isTimelineNeedProcessed")]
        public bool IsTimelineNeedProcessed { get; set; } = false;

        [BsonElement("isTimelineCompleted")]
        [JsonProperty("isTimelineCompleted")]
        public bool IsTimelineCompleted { get; set; } = false;

        [BsonElement("isReferralRuleExist")]
        [JsonProperty("isReferralRuleExist")]
        public bool IsReferralRuleExist { get; set; } = false;

        [BsonElement("referralRule")]
        [JsonProperty("referralRule")]
        public ReferralRuleModel.ReferralRule ReferralRule { get; set; } = null;

        [BsonElement("customerTimelines")]
        [JsonProperty("customerTimelines")]
        public List<CustomerTimelineModel.CustomerTimeline> CustomerTimelines { get; set; } = null;

        [BsonElement("isReferred")]
        [JsonProperty("isReferred")]
        public bool IsReferred { get; set; } = false;

        [BsonElement("referralDetail")]
        [JsonProperty("referralDetail")]
        public ReferralModel.ReferralDetail ReferralDetail { get; set; } = null;
    }
    public class ProcessedTransaction
    {
        [BsonElement("transactionRequest")]
        [JsonProperty("transactionRequest")]
        public Transaction? TransactionRequest { get; set; } = null;

        [BsonElement("customer")]
        [JsonProperty("customer")]
        public CustomerModel.Customer? Customer { get; set; } = null;

        [BsonElement("matchedCampaigns")]
        [JsonProperty("matchedCampaigns")]
        public List<MatchedCampaign> MatchedCampaigns { get; set; } = new List<MatchedCampaign>();

        [BsonElement("isReferred")]
        [JsonProperty("isReferred")]
        public bool IsReferred { get; set; } = false;       

        [BsonElement("referralDetail")]
        [JsonProperty("referralDetail")]
        public ReferralModel.ReferralDetail? ReferralDetail { get; set; } = null;


        [BsonElement("isRecurrenceReward")]
        [JsonProperty("isRecurrenceReward")]
        public bool IsRecurrenceReward { get; set; } = false;

        [BsonElement("recurrenceRewardDetail")]
        [JsonProperty("recurrenceRewardDetail")]
        public RecurrenceModel.RecurrenceRewardDetail? RecurrenceRewardDetail { get; set; } = null;


    }
    public class MatchedCampaign
    {
        // TODO : add startdate and enddate field.
        [BsonElement("campaignId")]
        [JsonProperty("campaignId")]
        public string CampaignId { get; set; }

        [BsonElement("eventType")]
        [JsonProperty("eventType")]
        public string EventType { get; set; } //"Wallet Creation|Wallet Load|Spent|Bill Payment", // from transaction payload

        [BsonElement("childEventCode")]
        [JsonProperty("childEventCode")]
        public string ChildEventCode { get; set; } = null; // Child Event Code For GenericActivity

        [BsonElement("offerType")]
        [JsonProperty("offerType")]
        public string OfferType { get; set; } // "Activity|Payment|Hybrid", // from matched campaign

        [BsonElement("isLock")]
        [JsonProperty("isLock")]
        public bool IsLock { get; set; }  //true,  // if event is matched with lock or unlock criteria
                                          //
        [BsonElement("isUnlock")]
        [JsonProperty("isUnLock")]
        public bool IsUnLock { get; set; }

        [BsonElement("isDirect")]
        [JsonProperty("isDirect")]
        public bool IsDirect { get; set; } //false, // if event is matched with direct criteria.

        //public List<string> RewardType { get; set; } //: "Points|Cashback|Voucher", // RewardType's value from RewardOption object

        [BsonElement("rewardCriteria")]
        [JsonProperty("rewardCriteria")]
        public CampaignModel.RewardCriteria RewardCriteria { get; set; } // From Campaign Object

        [BsonElement("rewardOptions")]
        [JsonProperty("rewardOptions")]
        public List<CampaignModel.RewardOption> RewardOptions { get; set; } // From Campaign Object

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("startDate")]
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("endDate")]
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        [BsonElement("narration")]
        [JsonProperty("narration")]
        public string Narration { get; set; }

        [BsonElement("ctaUrl")]
        [JsonProperty("ctaUrl")]
        public string CTAUrl { get; set; } = null;

        [BsonElement("isOncePerCampaign")]
        [JsonProperty("isOncePerCampaign")]
        public bool IsOncePerCampaign { get; set; } = false;

        [BsonElement("onceInLifeTime")]
        [JsonProperty("onceInLifeTime")]
        public OnceInLifeTime OnceInLifeTime { get; set; } = null;        

        //[BsonElement("referralProgram")]
        //[JsonProperty("referralProgram")]
        //public CampaignModel.ReferralProgram ReferralProgram { get; set; } = null;

        [BsonElement("filter")]
        [JsonProperty("filter")]
        public CampaignModel.Filter Filter { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("applyProductCode")]
        [JsonProperty("applyProductCode")]
        public int ApplyProductCode { get; set; } = 0; // 0 = Yes, 1 = No, 2 = Any

        [BsonIgnoreIfNull]
        [BsonElement("productCode")]
        [JsonProperty("productCode")]
        public List<string> ProductCode { get; set; }
    }   
    public class TransactionResponse
    {
        public string Key { get; set; }
        public Transaction TransactionRequest { get; set; }
    }
    #endregion
    public class TimelineTransactionRequest
    {
        public string EventCode { get; set; } = String.Empty;
        public string ChildEventCode { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty; 
        public string CustomerId { get; set; } = String.Empty;
        public string Lob { get; set; } = String.Empty;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
