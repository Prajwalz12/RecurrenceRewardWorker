﻿using Domain.Models.CustomerModel;
using Domain.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CustomerModel = Domain.Models.CustomerModel;

namespace Domain.Services
{
    public class CustomerSummaryService : ICustomerSummaryService
    {
        private readonly IMongoCollection<CustomerModel.CustomerSummary> _mongoCollection;

        public CustomerSummaryService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.CustomerSummarySettings.DatabaseName);
            _mongoCollection = database.GetCollection<CustomerModel.CustomerSummary>(settings.CustomerSummarySettings.CollectionName);
        }

        public List<CustomerModel.CustomerSummary> Get() => _mongoCollection.Find(_customer => true).ToList();

        public CustomerModel.CustomerSummary Get(string id) => _mongoCollection.Find<CustomerModel.CustomerSummary>(_customer => _customer.LoyaltyId == id).FirstOrDefault();

        public CustomerModel.CustomerSummary Create(CustomerModel.CustomerSummary customer) { _mongoCollection.InsertOne(customer); return customer; }

        public void Update(string id, CustomerModel.CustomerSummary customer) => _mongoCollection.ReplaceOne(_customer => _customer.LoyaltyId == id, customer);

        public void Remove(CustomerModel.CustomerSummary customer) => _mongoCollection.DeleteOne(_customer => _customer.LoyaltyId == customer.LoyaltyId);

        public void Remove(string id) => _mongoCollection.DeleteOne(_customer => _customer.LoyaltyId == id);

        public CustomerModel.CustomerSummary GetByMobileNumber(string mobileNumber) => _mongoCollection.Find<CustomerModel.CustomerSummary>(_customer => _customer.MobileNumber == mobileNumber).FirstOrDefault();

        public CustomerModel.CustomerSummary GetByCustomerUniqueId(string customerUniqueId) => _mongoCollection.Find<CustomerModel.CustomerSummary>(_customerSummary => _customerSummary.UniqueCustomerId == customerUniqueId).FirstOrDefault();
    }
}
