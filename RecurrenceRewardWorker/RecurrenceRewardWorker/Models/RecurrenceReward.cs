using CampaignModel = Domain.Models.CampaignModel;
using ReferralModel = Domain.Models.ReferralModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Domain.Models.RecurrenceModel;

[BsonIgnoreExtraElements]
public class RecurrenceReward
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    [JsonProperty("id")]
    public string? RecurrenceRewardId { get; set; } = null;

    [BsonElement("referee")]
    [JsonProperty("referee")]
    public ReferralModel.Referee? Referee { get; set; } = null;

    [BsonElement("referrer")]
    [JsonProperty("referrer")]
    public ReferralModel.Referrer? Referrer { get; set; } = null;

    [BsonElement("cumulativeCampaignId")]
    [JsonProperty("cumulativeCampaignIdd")]
    public string? CumulativeCampaignId { get; set; } = null;

    [BsonElement("rewardType")]
    [JsonProperty("rewardType")]
    public string RewardType { get; set; } = "CASHBACK";

    [BsonElement("rewardCappingd")]
    [JsonProperty("rewardCapping")]
    public decimal RewardCapping { get; set; }

    [BsonElement("rewardedValue")]
    [JsonProperty("rewardedValue")]
    public decimal RewardedValue { get; set; }

    [BsonElement("createdBy")]
    [JsonProperty("createdBy")]
    public string CreatedBy { get; set; } = String.Empty;

    [BsonElement("updatedBy")]
    [JsonProperty("updatedBy")]
    public string UpdatedBy { get; set; } = String.Empty;

    [BsonElement("createdDate")]
    [JsonProperty("createdDate")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [BsonElement("updatedDate")]
    [JsonProperty("updatedDate")]
    public DateTime? UpdatedDate { get; set; } = null;
}
public class RecurrenceRewardQueueRequest
{
    [BsonElement("referee")]
    [JsonProperty("referee")]
    public ReferralModel.Referee? Referee { get; set; } = null;

    [BsonElement("referrer")]
    [JsonProperty("referrer")]
    public ReferralModel.Referrer? Referrer { get; set; } = null;

    [BsonElement("transactionId")]
    [JsonProperty("transactionId")]
    public string TransactionId { get; set; } = String.Empty;

    [BsonElement("transactionReferenceNumber")]
    [JsonProperty("transactionReferenceNumber")]
    public string TransactionReferenceNumber { get; set; } = String.Empty;

    [BsonElement("transactionDateTime")]
    [JsonProperty("transactionDateTime")]
    public DateTime? TransactionDateTime { get; set; } = null;

}
public class RecurrenceRewardDetail
{
    [BsonElement("rewardType")]
    [JsonProperty("rewardType")]
    public string RewardType { get; set; } = "CASHBACK";

    [BsonElement("rewardValue")]
    [JsonProperty("rewardValue")]
    public decimal RewardValue { get; set; } = 0;

    [BsonElement("rewardOption")]
    [JsonProperty("rewardOption")]
    public CampaignModel.RewardOption? RewardOption { get; set; } = null;
}
