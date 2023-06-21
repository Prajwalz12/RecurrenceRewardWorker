using Domain.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerTimelineModel = Domain.Models.CustomerTimelineModel;

namespace Domain.Services
{
    public class CustomerTimelineService : ICustomerTimelineService
    {
        private readonly IMongoCollection<CustomerTimelineModel.CustomerTimeline> _mongoCollection;

        public CustomerTimelineService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.CustomerTimelineSettings.DatabaseName);
            _mongoCollection = database.GetCollection<CustomerTimelineModel.CustomerTimeline>(settings.CustomerTimelineSettings.CollectionName);
        }

        //public List<CustomerTimelineModel.CustomerTimeline> Get() => _mongoCollection.Find(_customer => true).ToList();

        //public CustomerTimelineModel.CustomerTimeline Get(string id) => _mongoCollection.Find<CustomerTimelineModel.CustomerTimeline>(_customer => _customer.LoyaltyId == id).FirstOrDefault();

        public CustomerTimelineModel.CustomerTimeline Create(CustomerTimelineModel.CustomerTimeline customerTimeline) { _mongoCollection.InsertOne(customerTimeline); return customerTimeline; }

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

        public List<CustomerTimelineModel.CustomerTimeline> Get(FilterDefinition<CustomerTimelineModel.CustomerTimeline> filter)
        {
            return _mongoCollection.Find(filter).ToList();
        }

        public UpdateResult Update(FilterDefinition<CustomerTimelineModel.CustomerTimeline> filterDefinition, UpdateDefinition<CustomerTimelineModel.CustomerTimeline> updateDefinition)
        {
            return _mongoCollection.UpdateOne(filterDefinition, updateDefinition);
        }



    }
}
