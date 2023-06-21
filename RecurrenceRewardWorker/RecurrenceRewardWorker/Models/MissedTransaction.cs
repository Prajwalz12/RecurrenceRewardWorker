using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Domain.Models
{
    [BsonIgnoreExtraElements]
    public class MissedTransaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string? MissingTransactionId { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("transactionId")]
        [JsonProperty("transactionId")]
        public string? TransactionId { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("transactionReferenceNumber")]
        [JsonProperty("transactionReferenceNumber")]
        public string? TransactionReferenceNumber { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("mobileNumber")]
        [JsonProperty("mobileNumber")]
        public string? MobileNumber { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("isQueued")]
        [JsonProperty("isQueued")]
        public bool IsQueued { get; set; } = false;

        [BsonIgnoreIfNull]
        [BsonElement("queuedDateTime")]
        [JsonProperty("queuedDateTime")]
        public DateTime? QueuedDateTime { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("isReceivedOnQualificationService")]
        [JsonProperty("isReceivedOnQualificationService")]
        public bool IsReceivedOnQualificationService { get; set; } = false;

        [BsonIgnoreIfNull]
        [BsonElement("receivedOnQualificationServiceDateTime")]
        [JsonProperty("receivedOnQualificationServiceDateTime")]
        public DateTime? ReceivedOnQualificationServiceDateTime { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("isReceivedOnExecutionService")]
        [JsonProperty("isReceivedOnExecutionService")]
        public bool IsReceivedOnExecutionService { get; set; } = false;

        [BsonIgnoreIfNull]
        [BsonElement("receivedOnExecutionServiceDateTime")]
        [JsonProperty("receivedOnExecutionServiceDateTime")]
        public DateTime? ReceivedOnExecutionServiceDateTime { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("isPickedFromTolarance")]
        [JsonProperty("isPickedFromTolarance")]
        public bool IsPickedFromFaultTolarance { get; set; } = false;

        [BsonIgnoreIfNull]
        [BsonElement("processedTransaction")]
        [JsonProperty("processedTransaction")]
        public TransactionModel.ProcessedTransaction? ProcessedTransaction { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("transactionDateTime")]
        [JsonProperty("transactionDateTime")]
        public DateTime TransactionDateTime { get; set; }

    }
}
