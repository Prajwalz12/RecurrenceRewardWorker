using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Domain.Models.ReferralModel
{
    public class ReferralRequest
    {
        [BsonElement("id")]
        [JsonProperty("id")]
        public int? Id { get; set; }

        [BsonElement("referee")]
        [JsonProperty("referee")]
        public Referee Referee { get; set; }

        [BsonElement("referer")]
        [JsonProperty("referer")]
        public Referrer Referrer { get; set; }

        [BsonElement("referralCode")]
        [JsonProperty("referralCode")]
        public string ReferralCode { get; set; }

        [BsonElement("referralRuleId")]
        [JsonProperty("referralRuleId")]
        public string ReferralRuleId { get; set; }

        [BsonElement("status")]
        [JsonProperty("status")]
        public int? Status { get; set; } = null;

        [BsonElement("referralStatus")]
        [JsonProperty("referralStatus")]
        public int? ReferralStatus { get; set; } = null;

    }
    public class UpdateReferralRequest : ReferralRequest
    {
        [BsonElement("isExpired")]
        [JsonProperty("isExpired")]
        public bool? IsExpired { get; set; } = null;

        [BsonElement("updatedDate")]
        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }

    #region Referral Program
    public class Referral
    {
        [BsonElement("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [BsonElement("referenceId")]
        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        [BsonElement("source")]
        [JsonProperty("source")]
        public string Source { get; set; }

        [BsonElement("referee")]
        [JsonProperty("referee")]
        public Referee Referee { get; set; }

        [BsonElement("referrer")]
        [JsonProperty("referrer")]
        public Referrer Referrer { get; set; }

        [BsonElement("status")]
        [JsonProperty("status")]
        public int Status { get; set; } // 0 : PENDING, 1: SUCCESS, 2 : NOTAPPLICABLE, 

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("referredDate")]
        [JsonProperty("referredDate")]
        public DateTime? ReferredDate { get; set; } = null;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("expiryDate")]
        [JsonProperty("expiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [BsonElement("isExpired")]
        [JsonProperty("isExpired")]
        public bool IsExpired { get; set; }

        [BsonElement("statusChangedDate")]
        [JsonProperty("statusChangedDate")]
        public DateTime? StatusChangedDate { get; set; }

        [BsonElement("referralCode")]
        [JsonProperty("referralCode")]
        public string ReferralCode { get; set; }

        [BsonElement("branchCode")]
        [JsonProperty("branchCode")]
        public string BranchCode { get; set; }

        [BsonElement("employeeCode")]
        [JsonProperty("employeeCode")]
        public string EmployeeCode { get; set; }

        [BsonElement("createdBy")]
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("updatedBy")]
        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createdDate")]
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("updatedDate")]
        [JsonProperty("updatedDate")]
        public DateTime? UpdatedDate { get; set; }


        [BsonElement("referralStatus")]
        [JsonProperty("referralStatus")]
        public int ReferralStatus { get; set; }

        [BsonElement("campaignId")]
        [JsonProperty("campaignId")]
        public string CampaignId { get; set; }

        [BsonElement("referType")]
        [JsonProperty("referType")]
        public string ReferType { get; set; }

        [BsonElement("isNew")]
        [JsonProperty("isNew")]
        public bool IsNew { get; set; }
        
        [BsonElement("franchiseCode")]
        [JsonProperty("franchiseCode")]
        public string FranchiseCode { get; set; }

        [BsonElement("referralRuleId")]
        [JsonProperty("referralRuleId")]
        public string ReferralRuleId { get; set; }
    }

    public class ReferralCommon
    {

        [BsonElement("lob")]
        [JsonProperty("lob")]
        public string Lob { get; set; }

        [BsonElement("customerId")]
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [BsonElement("mobileNumber")]
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [BsonElement("email")]
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Referee : ReferralCommon
    {
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class Referrer : ReferralCommon
    {

    }
    
    #endregion
    public class ReferralDetail
    {
        public Referral Referral { get; set; } = new Referral();
        public CustomerModel.Customer ReferrerCustomer { get; set; } = new CustomerModel.Customer();


    }
}
