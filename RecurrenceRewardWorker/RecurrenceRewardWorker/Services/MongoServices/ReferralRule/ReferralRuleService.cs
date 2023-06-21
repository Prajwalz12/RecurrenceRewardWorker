using Domain.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReferralRuleModel = Domain.Models.ReferralRuleModel;

namespace Domain.Services
{
    public class ReferralRuleService : IReferralRuleService
    {
        private readonly IMongoCollection<ReferralRuleModel.ReferralRule> _mongoCollection;

        public ReferralRuleService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.ReferralRuleSettings.DatabaseName);
            _mongoCollection = database.GetCollection<ReferralRuleModel.ReferralRule>(settings.ReferralRuleSettings.CollectionName);
        }
        public List<ReferralRuleModel.ReferralRule> Get(FilterDefinition<ReferralRuleModel.ReferralRule> filter)
        {
            return _mongoCollection.Find(filter).ToList();
        }

    }
}
