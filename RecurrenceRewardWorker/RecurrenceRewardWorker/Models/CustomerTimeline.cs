using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ReferralRuleModel = Domain.Models.ReferralRuleModel;

namespace Domain.Models.CustomerTimelineModel
{
    #region Internal Customer Timeline Section
    public class CustomerTimelineManagerResponse
    {
        public List<CustomerTimeline> CustomerTimelines { get; set; } = new List<CustomerTimeline>();
        public ReferralRuleModel.ReferralRule ReferralRule { get; set; } = new ReferralRuleModel.ReferralRule();
        public bool CustomerTimelineStatus { get; set; } = true;
    }
    public class CustomerTimelineRequest
    {
        public Referrer Referrer { get; set; } = null;
        public Referee Referee { get; set; } = null;

        public string TimelineEventCode { get; set; } = String.Empty;
        public bool? Status { get; set; } = null;
    }
    #endregion

    #region External Customer Timeline Section
    [BsonIgnoreExtraElements]
    public class CustomerTimeline
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("indexNumber")]
        [JsonProperty("indexNumber")]
        public int IndexNumber { get; set; }

        [BsonElement("referrer")]
        [JsonProperty("referrer")]
        public Referrer Referrer { get; set; }

        [BsonElement("referee")]
        [JsonProperty("referee")]
        public Referee Referee { get; set; }

        [BsonElement("customerTimelineEvent")]
        [JsonProperty("customerTimelineEvent")]
        public CustomerTimelineEvent CustomerTimelineEvent { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("eventDate")]
        [JsonProperty("eventDate")]
        public DateTime? EventDate { get; set; } = null;

        [BsonElement("eventOccuranceCount")]
        [JsonProperty("eventOccuranceCount")]
        public int EventOccuranceCount { get; set; }

        [BsonElement("status")]
        [JsonProperty("status")]
        public bool Status { get; set; } = false;

        [BsonElement("processedStatus")]
        [JsonProperty("processedStatus")]
        public int ProcessedStatus { get; set; }

        [BsonElement("isDisplayOnPortal")]
        [JsonProperty("isDisplayOnPortal")]
        public bool IsDisplayOnPortal { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createdDate")]
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("createdBy")]
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("updatedDate")]
        [JsonProperty("updatedDate")]
        public DateTime? UpdatedDate { get; set; } = null;

        [BsonElement("updatedBy")]
        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }
    }

    public class ReferralCommon
    {
        [BsonElement("customerId")]
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [BsonElement("lob")]
        [JsonProperty("lob")]
        public string Lob { get; set; }
    }

    public class Referrer : ReferralCommon { }
    public class Referee : ReferralCommon 
    {
        [BsonElement("mobileNumber")]
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; } = String.Empty;

        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("email")]
        [JsonProperty("email")]
        public string Email { get; set; } = String.Empty;
    }

    public class CustomerTimelineEvent
    {
        [BsonElement("eventName")]
        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [BsonElement("eventCode")]
        [JsonProperty("eventCode")]
        public string EventCode { get; set; }
    }
    #endregion
}
