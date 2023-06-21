using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Models.CustomerTimelineEventMasterModel
{
    public class CustomerTimelineEventMasterCommon
    {       

        [BsonElement("lobName")]
        [JsonProperty("lobName")]
        public string LobName { get; set; }

        [BsonElement("lobCode")]
        [JsonProperty("lobCode")]
        public string LobCode { get; set; }

        [BsonElement("timelineEventName")]
        [JsonProperty("timelineEventName")]
        public string TimelineEventName { get; set; }

        [BsonElement("timelineEventCode")]
        [JsonProperty("timelineEventCode")]
        public string TimelineEventCode { get; set; }

        [BsonElement("status")]
        [JsonProperty("status")]
        public int Status { get; set; }

        [BsonElement("priority")]
        [JsonProperty("priority")]
        public int Priority { get; set; }

        [BsonElement("isDisplayOnPortal")]
        [JsonProperty("isDisplayOnPortal")]
        public bool IsDisplayOnPortal { get; set; }
    }

    public class CustomerTimelineEvent : CustomerTimelineEventMasterCommon
    {
        [BsonElement("id")]
        [JsonProperty("id")]
        public int Id { get; set; }
    }
    public class CustomerTimelineEventMaster 
    {
       public List<CustomerTimelineEvent> CustomerTimelineEvents { get; set; } = new List<CustomerTimelineEvent>();
    }
    public class CustomerTimelineEventMasterResponse : CustomerTimelineEventMasterCommon
    {
        
    }
    public class CustomerTimelineEventRequest
    {
        [BsonElement("lobCode")]
        [JsonProperty("lobCode")]
        public string LobCode { get; set; }

        [BsonElement("timelineEventCode")]
        [JsonProperty("timelineEventCode")]
        public string TimelineEventCode { get; set; }
    }
}
