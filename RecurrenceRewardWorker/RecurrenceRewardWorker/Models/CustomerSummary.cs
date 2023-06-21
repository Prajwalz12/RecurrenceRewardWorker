using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Domain.Models.CustomerModel
{
    public class PointCashback
    {
        [BsonElement("lifeTimeEarn")]
        [JsonProperty("lifeTimeEarn")]
        public double LifeTimeEarn { get; set; }

        [BsonElement("lifeTimeExpired")]
        [JsonProperty("lifeTimeExpired")]
        public double LifeTimeExpired { get; set; }

        [BsonElement("lifeTimeRedeemed")]
        [JsonProperty("lifeTimeRedeemed")]
        public double LifeTimeRedeemed { get; set; }

        [BsonElement("availableBalance")]
        [JsonProperty("availableBalance")]
        public double AvailableBalance { get; set; }

        [BsonElement("currentMonthEarned")]
        [JsonProperty("currentMonthEarned")]
        public double CurrentMonthEarned { get; set; }

        [BsonElement("currentMonthRedeemed")]
        [JsonProperty("currentMonthRedeemed")]
        public double CurrentMonthRedeemed { get; set; }

        [BsonElement("currentMonthExpired")]
        [JsonProperty("currentMonthExpired")]
        public double CurrentMonthExpired { get; set; }

        [BsonElement("expireInNext30Days")]
        [JsonProperty("expireInNext30Days")]
        public double ExpireInNext30Days { get; set; }
    }
    public class Point : PointCashback
    {
        [BsonElement("blockedPoints")]
        [JsonProperty("blockedPoints")]
        public double BlockedPoints { get; set; }

        [BsonElement("blockedPromoPoints")]
        [JsonProperty("blockedPromoPoints")]
        public double BlockedPromoPoints { get; set; }

        [BsonElement("promoPoints")]
        [JsonProperty("promoPoints")]
        public double PromoPoints { get; set; }
    }
    public class Cashback : PointCashback
    {

    }
    public class Voucher
    {
        [BsonElement("vouchersEarned")]
        [JsonProperty("vouchersEarned")]
        public int VouchersEarned { get; set; }

        [BsonElement("vouchersPurchased")]
        [JsonProperty("vouchersPurchased")]
        public int VouchersPurchased { get; set; }
    }
    public class CustomerSummary
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("loyaltyId")]
        [JsonProperty("loyaltyId")]
        public string LoyaltyId { get; set; }

        [BsonElement("mobileNumber")]
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [BsonElement("points")]
        [JsonProperty("points")]
        public Point Point { get; set; }

        [BsonElement("cashback")]
        [JsonProperty("cashback")]
        public Cashback Cashback { get; set; }

        [BsonElement("vouchers")]
        [JsonProperty("vouchers")]
        public Voucher Voucher { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("createdOn")]
        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("updatedOn")]
        [JsonProperty("updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("firstTransactionDate")]
        [JsonProperty("firstTransactionDate")]
        public DateTime? FirstTransactionDate { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("lastTransactionDate")]
        [JsonProperty("lastTransactionDate")]
        public DateTime? LastTransactionDate { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("lob")]
        [JsonProperty("lob")]
        public string Lob { get; set; }

        [BsonElement("ucIc")]
        [JsonProperty("ucIc")]
        public string UcIc { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("referralCode")]
        [JsonProperty("referralCode")]
        public string ReferralCode { get; set; }

        [BsonElement("externalCustomerId")]
        [JsonProperty("externalCustomerId")]
        public string ExternalCustomerId { get; set; }
        [BsonElement("internalCustomerId")]
        [JsonProperty("internalCustomerId")]
        public string InternalCustomerId { get; set; }

        [BsonElement("uniqueCustomerId")]
        [JsonProperty("uniqueCustomerId")]
        public string UniqueCustomerId { get; set; }
    }
}
