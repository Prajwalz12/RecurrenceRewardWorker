using Domain.Models.Common;
using Domain.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CustomerModel = Domain.Models.CustomerModel;

namespace Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<CustomerModel.Customer> _mongoCollection;

        public CustomerService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.CustomerSettings.DatabaseName);
            _mongoCollection = database.GetCollection<CustomerModel.Customer>(settings.CustomerSettings.CollectionName);
        }

        public List<CustomerModel.Customer> Get() => _mongoCollection.Find(_customer => true).ToList();

        public CustomerModel.Customer Get(string id) => _mongoCollection.Find<CustomerModel.Customer>(_customer => _customer.LoyaltyId == id).FirstOrDefault();

        public CustomerModel.Customer Create(CustomerModel.Customer customer) { _mongoCollection.InsertOne(customer); return customer; }

        public void Update(string id, CustomerModel.Customer customer) => _mongoCollection.ReplaceOne(_customer => _customer.LoyaltyId == id, customer);

        public void Remove(CustomerModel.Customer customer) => _mongoCollection.DeleteOne(_customer => _customer.LoyaltyId == customer.LoyaltyId);

        public void Remove(string id) => _mongoCollection.DeleteOne(_customer => _customer.LoyaltyId == id);

        public CustomerModel.Customer GetByMobileNumber(string mobileNumber) => _mongoCollection.Find<CustomerModel.Customer>(_customer => _customer.MobileNumber == mobileNumber).FirstOrDefault();

        public UpdateResult Update(string loyaltyId, Wallet wallet)
        {
            var filter = Builders<CustomerModel.Customer>.Filter.Where(o=> o.LoyaltyId == loyaltyId);
            var updateQuery = Builders<CustomerModel.Customer>.Update.Set(o=> o.Wallet.Id, wallet.Id).Set(o=> o.Flags.Wallet, true);
            return _mongoCollection.UpdateOne(filter, updateQuery);
        }
        public List<CustomerModel.Customer> Get(FilterDefinition<CustomerModel.Customer> filterDefinition)
        {
            return _mongoCollection.Find(filterDefinition).ToList();
        }
    }
}
