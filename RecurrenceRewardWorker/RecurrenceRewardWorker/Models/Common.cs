using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Domain.Models.Common
{
    public class VPA
    {
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createdDateTime")]
        [JsonProperty("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [BsonElement("status")]
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class OnboardingIdentifier
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [BsonElement("value")]
        [JsonProperty("value")]
        public string Value { get; set; }
    }
    public class CustomerCommonProperty
    {
        [BsonElement("kycUpgradeFlg")]
        [JsonProperty("kycUpgradeFlg")]
        // [RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper KYCUpgradeFlg")]
        public string KYCUpgradeFlg { get; set; }

        [BsonElement("destinationMobile")]
        [JsonProperty("destinationMobile")]
        //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper DestinationMobile")]
        public string DestinationMobile { get; set; }

        [BsonElement("destinationVPAId")]
        [JsonProperty("destinationVPAId")]
        //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper DestinationVPAId")]
        public string DestinationVPAId { get; set; }

        [BsonElement("customerId")]
        [JsonProperty("customerId")]
        //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Customer id")]
        public string CustomerId { get; set; }
    }
    public class Wallet
    {
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createdDateTime")]
        [JsonProperty("createdDateTime")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("loadDateTime")]
        [JsonProperty("loadDateTime")]
        public DateTime LoadDateTime { get; set; } = DateTime.Now;

        [BsonElement("balance")]
        [JsonProperty("balance")]
        public Double Balance { get; set; }


    }
    namespace CustomerModel
    {
        #region Customer
        public class Flag
        {
            [BsonElement("wallet")]
            [JsonProperty("wallet")]
            public bool Wallet { get; set; }

            [BsonElement("dormant")]
            [JsonProperty("dormant")]
            public bool Dormant { get; set; }

            [BsonIgnoreIfNull]
            [BsonElement("lobFraud")]
            [JsonProperty("lobFraud")]
            public List<string> LobFraud { get; set; } = new List<string>();

            [BsonElement("loyaltyFraud")]
            [JsonProperty("loyaltyFraud")]
            public int LoyaltyFraud { get; set; }

            [BsonElement("isWhitelisted")]
            [JsonProperty("isWhitelisted")]
            public bool? IsWhitelisted { get; set; }

            [BsonElement("globalDeliquient")]
            [JsonProperty("globalDeliquient")]
            public bool GlobalDeliquient { get; set; }

            [BsonElement("customerId")]
            [JsonProperty("customerId")]
            public bool CustomerId { get; set; }
        }
        public class GlobalDeliquient
        {
            [BsonElement("id")]
            [JsonProperty("id")]
            public string Id { get; set; }
        }
        public class KYC
        {
            [BsonElement("status")]
            [JsonProperty("status")]
            public int Status { get; set; }

            [BsonElement("completionTag")]
            [JsonProperty("completionTag")]
            public string CompletionTag { get; set; } = String.Empty;

            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            [BsonElement("completedDateTime")]
            [JsonProperty("completedDateTime")]
            public DateTime CompletedDateTime { get; set; } = DateTime.Now;

            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            [BsonElement("prevDate")]
            [JsonProperty("prevDate")]
            public DateTime PrevDate { get; set; } = DateTime.Now;
            //public string Date { get; set; }
        }
        public class Channel
        {
            [BsonElement("source")]
            [JsonProperty("source")]
            public string Source { get; set; }

            [BsonElement("medium")]
            [JsonProperty("medium")]
            public string Medium { get; set; }
        }
        public class Install
        {
            [BsonElement("source")]
            [JsonProperty("source")]
            public string Source { get; set; }

            [BsonIgnoreIfNull]
            [BsonElement("sourceId")]
            [JsonProperty("sourceId")]
            public int? SourceId { get; set; }

            [BsonElement("channel")]
            [JsonProperty("channel")]
            public string Channel { get; set; }

            [BsonIgnoreIfNull]
            [BsonElement("channelId")]
            [JsonProperty("channelId")]
            public int? ChannelId { get; set; }
        }
        public class Segment
        {
            [BsonElement("code")]
            [JsonProperty("code")]
            public string Code { get; set; } = string.Empty;

            [BsonElement("type")]
            [JsonProperty("type")]
            public string Type { get; set; } = "DYNAMIC";
        }
        #endregion
    }
    namespace TransactionModel
    {
        #region Transaction
        public class Wallet
        {
            [BsonElement("id")]
            [JsonProperty("id")]
            public string Id { get; set; }

            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            [BsonIgnoreIfNull]
            [BsonElement("createdDateTime")]
            [JsonProperty("createdDateTime")]
            public DateTime? CreatedDateTime { get; set; } = DateTime.Now;

            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            [BsonIgnoreIfNull]
            [BsonElement("loadDateTime")]
            [JsonProperty("loadDateTime")]
            public DateTime? LoadDateTime { get; set; } = DateTime.Now;
        }
        public class Campaign
        {
            //[BsonRepresentation(BsonType.ObjectId)]
            [BsonElement("id")]
            [JsonProperty(PropertyName = "id")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Id")]
            public string Id { get; set; }

            [BsonElement("rewardedFlg")]
            [JsonProperty("rewardedFlg")]
            public bool RewardedFlg { get; set; }
        }
        public class QR
        {
            [BsonElement("version")]
            [JsonProperty("version")]
            public string Version { get; set; }

            [BsonElement("scanFlg")]
            [JsonProperty("scanFlg")]
            public bool ScanFlg { get; set; }
        }

        public class EMI
        {
            [BsonElement("bounceFlg")]
            [JsonProperty("bounceFlg")]
            public bool BounceFlg { get; set; }

            [BsonElement("amount")]
            [JsonProperty("amount")]
            public double Amount { get; set; }
        }

        public class CustomerDetail
        {
            [BsonElement("loyaltyId")]
            [JsonProperty("loyaltyId")]
            //[BsonRepresentation(BsonType.ObjectId)]
            public string LoyaltyId { get; set; }

            [BsonElement("customerVersionId")]
            [JsonProperty("customerVersionId")]
            //[BsonRepresentation(BsonType.ObjectId)]
            public string CustomerVersionId { get; set; }

        }
        public class Customer : CustomerCommonProperty
        {
            [BsonElement("mobileNumber")]
            [JsonProperty("mobileNumber")]
            public string MobileNumber { get; set; }

            [BsonElement("vpa")]
            [JsonProperty("vpa")]
            public Common.VPA VPA { get; set; }
        }
        public class Voucher
        {
            [BsonElement("code")]
            [JsonProperty("code")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Voucher code")]
            public string Code { get; set; }

            [BsonElement("type")]
            [JsonProperty("type")]
            // [RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Voucher type")]
            public string Type { get; set; }

            [BsonElement("denomination")]
            [JsonProperty("denomination")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Voucher denomination")]
            public string Denomination { get; set; }
        }
        public class Payment
        {

            [BsonElement("paymentInstrument")]
            [JsonProperty("paymentInstrument")]
            public string PaymentInstrument { get; set; }

            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            [BsonElement("paymentDate")]
            [JsonProperty("paymentDate")]
            public DateTime? PaymentDate { get; set; } = DateTime.Now;

            [BsonElement("paymentMode")]
            [JsonProperty("paymentMode")]
            public string PaymentMode { get; set; }

            [BsonElement("amount")]
            [JsonProperty("amount")]
            public double Amount { get; set; }
        }

        public class UTM
        {
            [BsonElement("source")]
            [JsonProperty("source")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Source")]
            public string Source { get; set; }

            [BsonElement("campaign")]
            [JsonProperty("campaign")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Campaign")]
            public string Campaign { get; set; }

            [BsonElement("medium")]
            [JsonProperty("medium")]
            //[RegularExpression(@"^[.a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Medium")] /// Note : . is Intentionaly Allowed
            public string Medium { get; set; }
        }

        public class Biller
        {
            [BsonElement("id")]
            [JsonProperty("id")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Id")]
            public string Id { get; set; }

            [BsonElement("category")]
            [JsonProperty("category")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Category")]
            public string Category { get; set; }
        }

        public class MerchantOrDealer
        {
            [BsonElement("groupId")]
            [JsonProperty("groupId")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper GroupId")]
            public string GroupId { get; set; }

            [BsonElement("id")]
            [JsonProperty("id")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Merchant Or Dealer Id")]
            public string Id { get; set; }

            [BsonElement("category")]
            [JsonProperty("category")]
            //[RegularExpression(@"^[a-zA-Z0-9 _-]+$", ErrorMessage = "please enter a proper Merchant Or Dealer Category")]
            public string Category { get; set; }
        }

        public class TransactionDetail
        {
            [BsonElement("refNumber")]
            [JsonProperty("refNumber")]
            public string RefNumber { get; set; }

            [BsonElement("isRedeem")]
            [JsonProperty("isRedeem")]
            public bool IsRedeem { get; set; }

            [BsonElement("type")]
            [JsonProperty("type")]
            public string Type { get; set; } // Holds Payment

            [BsonElement("status")]
            [JsonProperty("status")]
            public string Status { get; set; }

            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            [BsonElement("dateTime")]
            [JsonProperty("dateTime")]
            public DateTime DateTime { get; set; } = DateTime.Now;

            [BsonElement("amount")]
            [JsonProperty("amount")]
            public double Amount { get; set; }

            [BsonElement("transactionMode")]
            [JsonProperty("transactionMode")]
            public string TransactionMode { get; set; }

            [BsonElement("biller")]
            [JsonProperty("biller")]
            public Common.TransactionModel.Biller Biller { get; set; }

            [BsonElement("merchantOrDealer")]
            [JsonProperty("merchantOrDealer")]
            public Common.TransactionModel.MerchantOrDealer MerchantOrDealer { get; set; }

            [BsonElement("qr")]
            [JsonProperty("qr")]
            public Common.TransactionModel.QR QR { get; set; }

            [BsonElement("emi")]
            [JsonProperty("emi")]
            public Common.TransactionModel.EMI EMI { get; set; }

            [BsonElement("wallet")]
            [JsonProperty("wallet")]
            public Common.TransactionModel.Wallet Wallet { get; set; }

            [BsonElement("customer")]
            [JsonProperty("customer")]
            public Common.TransactionModel.Customer Customer { get; set; }

            [BsonElement("voucher")]
            [JsonProperty("voucher")]
            public Common.TransactionModel.Voucher Voucher { get; set; }

            [BsonElement("payments")]
            [JsonProperty("payments")]
            public List<Common.TransactionModel.Payment> Payments { get; set; }

            [BsonElement("branchCode")]
            [JsonProperty("branchCode")]
            public string BranchCode { get; set; }

            [BsonElement("employeeCode")]
            [JsonProperty("employeeCode")]
            public string EmployeeCode { get; set; }

            [BsonElement("productCode")]
            [JsonProperty("productCode")]
            public string ProductCode { get; set; }

            //[BsonElement("lan")]
            //[JsonProperty("lan")]
            //public string LAN { get; set; }

            //[BsonElement("opportunityId")]
            //[JsonProperty("opportunityId")]
            //public string OpportunityId { get; set; }


            [BsonElement("loanType")]
            [JsonProperty("loanType")]
            public LoanType LoanType { get; set; }

            [BsonElement("emiType")]
            [JsonProperty("emiType")]
            public EMIType EmiType { get; set; }


            [BsonElement("tradingType")]
            [JsonProperty("tradingType")]
            public TradingType TradingType { get; set; }

            [BsonElement("loanData")]
            [JsonProperty("loanData")]
            public LoanData LoanData { get; set; }

        }
        public class LoanType
        {
            [BsonElement("type")]
            [JsonProperty("type")]
            public string Type { get; set; }

        }
        public class EMIType
        {
            [BsonElement("type")]
            [JsonProperty("type")]
            public string Type { get; set; }

        }
        public class TradingType
        {
            [BsonElement("type")]
            [JsonProperty("type")]
            public string Type { get; set; }

        }
        public class LoanData
        {
            [BsonElement("lan")]
            [JsonProperty("lan")]
            public string LAN { get; set; }

            [BsonElement("disbursalDate")]
            [JsonProperty("disbursalDate")]
            public string DisbursalDate { get; set; }


            [BsonElement("closureDate")]
            [JsonProperty("closureDate")]
            public string ClosureDate { get; set; }

            [BsonElement("foreclosureDate")]
            [JsonProperty("foreclosureDate")]
            public string ForeclosureDate { get; set; }

            [BsonElement("opportunityId")]
            [JsonProperty("opportunityId")]
            public string OpportunityId { get; set; }

            [BsonElement("disbursalType")]
            [JsonProperty("disbursalType")]
            public string DisbursalType { get; set; }

        }
        public class Referral
        {

            [BsonElement("code")]
            [JsonProperty("code")]
            public string Code { get; set; }

            [BsonElement("lob")]
            [JsonProperty("lob")]
            public string Lob { get; set; }

        }
        public class InvestmentDetails
        {
            [BsonElement("subscriptionType")]
            [JsonProperty("subscriptionType")]
            public string SubscriptionType { get; set; }

            [BsonElement("investorType")]
            [JsonProperty("investorType")]
            public string InvestorType { get; set; }

            [BsonElement("transferType")]
            [JsonProperty("transferType")]
            public string TransferType { get; set; }

            [BsonElement("sipType")]
            [JsonProperty("sipType")]
            public string SIPType { get; set; }

            [BsonElement("brokerageAmount")]
            [JsonProperty("brokerageAmount")]
            public string BrokerageAmount { get; set; }

            [BsonElement("investmentType")]
            [JsonProperty("investmentType")]
            public string InvestmentType { get; set; }

            [BsonElement("premiumType")]
            [JsonProperty("premiumType")]
            public string PremiumType { get; set; }

        }
        #endregion
    }
}
