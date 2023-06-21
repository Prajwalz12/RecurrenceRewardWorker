using CustomerTimelineModel = Domain.Models.CustomerTimelineModel;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface ICustomerTimelineService
    {
        List<CustomerTimelineModel.CustomerTimeline> Get(FilterDefinition<CustomerTimelineModel.CustomerTimeline> filter);
        CustomerTimelineModel.CustomerTimeline Create(CustomerTimelineModel.CustomerTimeline customerTimeline);
        UpdateResult Update(FilterDefinition<CustomerTimelineModel.CustomerTimeline> filterDefinition, UpdateDefinition<CustomerTimelineModel.CustomerTimeline> updateDefinition);
    }
}