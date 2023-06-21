using CustomerModel = Domain.Models.CustomerModel;
using Common = Domain.Models.Common;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Domain.Services
{
    public interface ICustomerService
    {
        CustomerModel.Customer Create(CustomerModel.Customer customer);
        List<CustomerModel.Customer> Get();
        List<CustomerModel.Customer> Get(FilterDefinition<CustomerModel.Customer> filterDefinition);
        CustomerModel.Customer Get(string id);
        CustomerModel.Customer GetByMobileNumber(string mobileNumber);
        void Remove(CustomerModel.Customer customer);
        void Remove(string id);
        void Update(string id, CustomerModel.Customer customer);
        UpdateResult Update(string loyaltyId, Common.Wallet wallet);
    }
}
