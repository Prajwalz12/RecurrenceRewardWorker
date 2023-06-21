using Domain.Models.GlobalConfigurationModel;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IGlobalConfigurationService
    {
        List<GlobalConfiguration> Get();
        List<GlobalConfiguration> Get(FilterDefinition<GlobalConfiguration> filter);
    }
}