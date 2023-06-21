using Domain.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using GlobalConfigurationModel = Domain.Models.GlobalConfigurationModel;

namespace Domain.Services
{
    public class GlobalConfigurationService : IGlobalConfigurationService
    {
        private readonly IMongoCollection<GlobalConfigurationModel.GlobalConfiguration> _mongoCollection;

        public GlobalConfigurationService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.GlobalConfigurationSettings.DatabaseName);
            _mongoCollection = database.GetCollection<GlobalConfigurationModel.GlobalConfiguration>(settings.GlobalConfigurationSettings.CollectionName);
        }

        public List<GlobalConfigurationModel.GlobalConfiguration> Get()
        {
            FilterDefinition<GlobalConfigurationModel.GlobalConfiguration> filter = Builders<GlobalConfigurationModel.GlobalConfiguration>.Filter.Where(o => o.IsActive && o.IsPublished);
            return _mongoCollection.Find(filter).ToList();
        }

        //public CustomerTimelineModel.CustomerTimeline Get(string id) => _mongoCollection.Find<CustomerTimelineModel.CustomerTimeline>(_customer => _customer.LoyaltyId == id).FirstOrDefault();

        //public GlobalConfigurationModel.GlobalConfiguration Create(CustomerTimelineModel.CustomerTimeline customerTimeline) { _mongoCollection.InsertOne(customerTimeline); return customerTimeline; }

        //public void Update(string id, CustomerTimelineModel.CustomerTimeline customerTimeline) => _mongoCollection.ReplaceOne(_customer => _customer.LoyaltyId == id, customer);

        //public void Remove(CustomerTimelineModel.CustomerTimeline customerTimeline) => _mongoCollection.DeleteOne(_customer => _customer.LoyaltyId == customer.LoyaltyId);

        //public void Remove(string id) => _mongoCollection.DeleteOne(_customer => _customer.LoyaltyId == id);

        // public CustomerTimelineModel.CustomerTimeline GetByMobileNumber(string mobileNumber) => _mongoCollection.Find<CustomerTimelineModel.CustomerTimeline>(_customer => _customer.MobileNumber == mobileNumber).FirstOrDefault();

        /* public UpdateResult Update(string loyaltyId, Wallet wallet)
         {
             var filter = Builders<CustomerModel.Customer>.Filter.Where(o => o.LoyaltyId == loyaltyId);
             var updateQuery = Builders<CustomerModel.Customer>.Update.Set(o => o.Wallet.Id, wallet.Id).Set(o => o.Flags.Wallet, true);
             return _mongoCollection.UpdateOne(filter, updateQuery);
         }*/

        public List<GlobalConfigurationModel.GlobalConfiguration> Get(FilterDefinition<GlobalConfigurationModel.GlobalConfiguration> filter)
        {
            return _mongoCollection.Find(filter).ToList();
        }

        //public UpdateResult Update(FilterDefinition<CustomerTimelineModel.CustomerTimeline> filterDefinition, UpdateDefinition<CustomerTimelineModel.CustomerTimeline> updateDefinition)
        //{
        //    return _mongoCollection.UpdateOne(filterDefinition, updateDefinition);
        //}
    }
}
