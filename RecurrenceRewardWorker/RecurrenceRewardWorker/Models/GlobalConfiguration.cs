using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Domain.Models.GlobalConfigurationModel
{
    public class GlobalConfiguration
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("isActive")]
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("isPublished")]
        [JsonProperty("isPublished")]
        public bool IsPublished { get; set; }

        [BsonElement("rateConfiguration")]
        [JsonProperty("rateConfiguration")]
        public RateConfiguration RateConfiguration { get; set; }

        [BsonElement("referralConfiguration")]
        [JsonProperty("referralConfiguration")]
        public ReferralConfiguration ReferralConfiguration { get; set; }


        [BsonElement("transactionConfiguration")]
        [JsonProperty("transactionConfiguration")]
        public TransactionConfiguration TransactionConfiguration { get; set; }
    }

    public class RateConfiguration
    {
        [BsonElement("pointRate")]
        [JsonProperty("pointRate")]
        public int PointRate { get; set; }
    }

    public class ReferralConfiguration
    {
        [BsonElement("isReferralApplicable")]
        [JsonProperty("isReferralApplicable")]
        public bool IsReferralApplicable { get; set; }

        [BsonElement("isReferralRuleApplicable")]
        [JsonProperty("isReferralRuleApplicable")]
        public bool IsReferralRuleApplicable { get; set; }

        [BsonElement("isTimelineApplicable")]
        [JsonProperty("isTimelineApplicable")]
        public bool IsTimelineApplicable { get; set; }

        [BsonElement("isReferralExpiryRule")]
        [JsonProperty("isReferralExpiryRule")]
        public bool IsReferralExpiryRule { get; set; }

        [BsonElement("referralExpiryRule")]
        [JsonProperty("referralExpiryRule")]
        public ReferralExpiryRule ReferralExpiryRule { get; set; }


        //[BsonElement("expiryDate")]
        //[JsonProperty("expiryDate")]
        //public DateTime ExpiryDate { get; set; }
    }

    public class ReferralExpiryRule
    {
        [BsonElement("from")]
        [JsonProperty("from")]
        public string From { get; set; }

        [BsonElement("duration")]
        [JsonProperty("duration")]
        public int Duration { get; set; }
    }
    public class TransactionConfiguration
    {
        [BsonElement("transactionNotSentByClient")]
        [JsonProperty("transactionNotSentByClient")]
        public TransactionNotSentByClient TransactionNotSentByClient { get; set; }

    }
    public class TransactionNotSentByClient
    {
        [BsonElement("eventCodes")]
        [JsonProperty("eventCodes")]
        public List<EventCodes> EventCodes { get; set; }
    }
    public class EventCodes
    {
        [BsonElement("eventCode")]
        [JsonProperty("eventCode")]
        public EventCode EventCode { get; set; }
    }
    public class EventCode
    {
        [BsonElement("code")]
        [JsonProperty("code")]
        public string Code { get; set; }

        [BsonElement("childEventCodes")]
        [JsonProperty("childEventCodes")]
        public List<String> ChildEventCodes { get; set; }

        [BsonElement("types")]
        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }
}
