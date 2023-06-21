using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Domain.Models.CustomerModel
{

    public class CustomerResponse
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("responseData")]
        public ResponseData responseData { get; set; }
    }

    public class CustomerRequest
    {
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string LoyaltyId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("customerVersionId")]
        [JsonProperty("customerVersionId")]
        public string CustomerVersionId { get; set; }

        [BsonElement("mobileNumber")]
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [BsonElement("upiId")]
        [JsonProperty("upiId")]
        public string UPIId { get; set; }

        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [BsonElement("typeId")]
        [JsonProperty("typeId")]
        public int TypeId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("signUpDate")]
        [JsonProperty("signUpDate")]
        public DateTime SignUpDate { get; set; } = DateTime.Now;

        [BsonElement("wallet")]
        [JsonProperty("wallet")]
        public Common.Wallet Wallet { get; set; }

        [BsonElement("walletBalance")]
        [JsonProperty("walletBalance")]
        public double WalletBalance { get; set; }

        [BsonElement("vpa")]
        [JsonProperty("vpa")]
        public Common.VPA VPA { get; set; }

        [BsonElement("kyc")]
        [JsonProperty("kyc")]
        public Common.CustomerModel.KYC KYC { get; set; }

        //[BsonIgnoreIfNull]
        [BsonElement("flags")]
        [JsonProperty("flags")]
        public Common.CustomerModel.Flag Flags { get; set; }

        [BsonElement("install")]
        [JsonProperty("install")]
        public Common.CustomerModel.Install Install { get; set; }

        [BsonElement("segments")]
        [JsonProperty("segments")]
        public List<Common.CustomerModel.Segment> Segments { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createdAt")]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("updatedAt")]
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [BsonElement("referralCode")]
        [JsonProperty("referralCode")]
        public string ReferralCode { get; set; }

        [BsonElement("lob")]
        [JsonProperty("lob")]
        public string Lob { get; set; }

        [BsonElement("ucIc")]
        [JsonProperty("ucIc")]
        public string UcIc { get; set; }

        [BsonElement("emailId")]
        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("referenceNumber")]
        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("dateOfBirth")]
        [JsonProperty("dateOfBirth")]
        public DateTime? DateOfBirth { get; set; } = null;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("anniversary")]
        [JsonProperty("anniversary")]
        public DateTime? Anniversary { get; set; } = null;

        [BsonElement("uniqueCustomerId")]
        [JsonProperty("uniqueCustomerId")]
        public string UniqueCustomerId { get; set; }

        [BsonElement(elementName: "customerId")]
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [BsonElement(elementName: "mode")]
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement(elementName: "accountOpeningDate")]
        [JsonProperty("accountOpeningDate")]
        public DateTime? AccountOpeningDate { get; set; }

        [BsonElement(elementName: "onboardingIdentifier")]
        [JsonProperty("onboardingIdentifier")]
        public Common.OnboardingIdentifier OnboardingIdentifier { get; set; }

        [BsonElement("sourceChannelId")]
        [JsonProperty("sourceChannelId")]
        public int? SourceChannelId { get; set; }

        [BsonElement("isInfluencer")]
        [JsonProperty("isInfluencer")]
        public bool IsInfluencer { get; set; } = false;
    }
    public class ResponseData
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }
    }    
}
