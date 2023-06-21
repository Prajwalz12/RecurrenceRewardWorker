using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DefaultDatabaseName { get; set; }
        public CustomerSettings CustomerSettings { get; set; }
        public CustomerEventSettings CustomerEventSettings { get; set; }
        public CustomerVersionSettings CustomerVersionSettings { get; set; }
        public TransactionSettings TransactionSettings { get; set; }
        public TransactionRewardSettings TransactionRewardSettings { get; set; }
        public CampaignSettings CampaignSettings { get; set; }
        public LoyaltyFraudManagerSettings LoyaltyFraudManagerSettings { get; set; }
        public OfferMapSettings OfferMapSettings  { get; set; }
        public CumulativeTransactionSettings CumulativeTransactionSettings { get; set; }
        public CustomerSummarySettings CustomerSummarySettings { get; set; }
        public GroupCampaignTransactionSettings GroupCampaignTransactionSettings { get; set; }
        public MissedTransactionRequestSettings MissedTransactionRequestSettings { get; set; }
        public CustomerTimelineSettings CustomerTimelineSettings { get; set; }
        public ReferralRuleSettings ReferralRuleSettings { get; set; }
        public GlobalConfigurationSettings GlobalConfigurationSettings { get; set; }
        public RecurrenceRewardSetting RecurrenceRewardSetting { get; set; }

    }

    public class GlobalConfigurationSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class OfferMapSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }

    public class CustomerEventSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }

    public class CustomerSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class CustomerSummarySettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class CustomerVersionSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class TransactionSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class TransactionRewardSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class CampaignSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class LoyaltyFraudManagerSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class CumulativeTransactionSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class GroupCampaignTransactionSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class MissedTransactionRequestSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }

    public class CustomerTimelineSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }

    public class ReferralRuleSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public class RecurrenceRewardSetting
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DefaultDatabaseName { get; set; }
        CustomerSettings CustomerSettings { get; set; }
        CustomerEventSettings CustomerEventSettings { get; set; }
        CustomerVersionSettings CustomerVersionSettings { get; set; }
        TransactionSettings TransactionSettings { get; set; }
        TransactionRewardSettings TransactionRewardSettings { get; set; }
        CampaignSettings CampaignSettings { get; set; }
        LoyaltyFraudManagerSettings LoyaltyFraudManagerSettings { get; set; }
        OfferMapSettings OfferMapSettings { get; set; }
        CumulativeTransactionSettings CumulativeTransactionSettings { get; set; }
        CustomerSummarySettings CustomerSummarySettings { get; set; }
        GroupCampaignTransactionSettings GroupCampaignTransactionSettings { get; set; }
        MissedTransactionRequestSettings MissedTransactionRequestSettings { get; set; }
        CustomerTimelineSettings CustomerTimelineSettings { get; set; }
        ReferralRuleSettings ReferralRuleSettings { get; set; }
        GlobalConfigurationSettings GlobalConfigurationSettings { get; set; }
        RecurrenceRewardSetting RecurrenceRewardSetting { get; set; }
    }

}
