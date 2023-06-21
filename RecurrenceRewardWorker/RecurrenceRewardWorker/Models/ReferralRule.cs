using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Domain.Models.ReferralRuleModel;

#region Internal ReferralRule Section
public class ReferralRuleRequest
{
    [BsonElement("lobCode")]
    [JsonProperty("lobCode")]
    public string LobCode { get; set; }

    [BsonElement("dynamicSegment")]
    [JsonProperty("dynamicSegment")]
    public string DynamicSegmentCode { get; set; }

    [BsonElement("referralRuleId")]
    [JsonProperty("referralRuleId")]
    public string ReferralRuleId { get; set; }

    [BsonElement("customer")]
    [JsonProperty("customer")]
    public CustomerModel.Customer Customer { get; set; }

}
#endregion
#region External ReferralRule Section
[BsonIgnoreExtraElements]
public class ReferralRule
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    [JsonProperty("id")]
    public string ReferralRuleId { get; set; }

    [BsonElement("ruleName")]
    [JsonProperty("ruleName")]
    public string RuleName { get; set; }

    [BsonElement("lob")]
    [JsonProperty("lob")]
    public string Lob { get; set; }

    [BsonElement("dynamicSegment")]
    [JsonProperty("dynamicSegment")]
    public string DynamicSegment { get; set; }

    [BsonElement("TimelineEvents")]
    [JsonProperty("TimelineEvents")]
    public List<ReferralTimelineEvent> TimeLineEvents { get; set; }

    [BsonElement("status")]
    [JsonProperty("status")]
    public string Status { get; set; }

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

    [BsonElement("updatedAt")]
    [JsonProperty("updatedAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime? UpdatedAt { get; set; }
}
public class ReferralTimelineEvent
{
    [BsonElement("indexNumber")]
    [JsonProperty("indexNumber")]
    public int IndexNumber { get; set; }

    [BsonElement("eventName")]
    [JsonProperty("eventName")]
    public string EventName { get; set; }

    [BsonElement("eventCode")]
    [JsonProperty("eventCode")]
    public string EventCode { get; set; }

    [BsonElement("isDisplayOnPortal")]
    [JsonProperty("isDisplayOnPortal")]
    public bool IsDisplayOnPortal { get; set; }

    [BsonElement("amount")]
    [JsonProperty("amount")]
    public ReferralAmount Amount { get; set; }

    [BsonElement("isReferenceCondition")]
    [JsonProperty("isReferenceCondition")]
    public bool IsReferenceCondition { get; set; }

    [BsonElement("referenceCondition")]
    [JsonProperty("referenceCondition")]
    public TimelineReferenceCondition ReferenceCondition { get; set; }
}
public class ReferralAmount
{
    [BsonElement("isApplied")]
    [JsonProperty("isApplied")]
    public bool IsApplied { get; set; }

    [BsonElement("minValue")]
    [JsonProperty("minValue")]
    public int MinValue { get; set; }
}
public class TimelineReferenceCondition
{
    [BsonElement("referenceEvent")]
    [JsonProperty("referenceEvent")]
    public TimelineReferenceEvent ReferenceEvent { get; set; }

    [BsonElement("referenceDuration")]
    [JsonProperty("referenceDuration")]
    public TimelineReferenceDuration ReferenceDuration { get; set; }
}
public class TimelineReferenceEvent
{
    [BsonElement("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [BsonElement("code")]
    [JsonProperty("code")]
    public string Code { get; set; }

    [BsonElement("type")]
    [JsonProperty("type")]
    public string Type { get; set; }

}
public class TimelineReferenceDuration
{
    [BsonElement("valueType")]
    [JsonProperty("valueType")]
    public string ValueType { get; set; } = "Day";

    [BsonElement("value")]
    [JsonProperty("value")]
    public int Value { get; set; }
}
#endregion
