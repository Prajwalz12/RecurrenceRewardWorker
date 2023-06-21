using MongoDB.Driver;
using System.Collections.Generic;

namespace Domain.Services
{ 
    public interface IReferralRuleService
    {
        List<Domain.Models.ReferralRuleModel.ReferralRule> Get(FilterDefinition<Domain.Models.ReferralRuleModel.ReferralRule> filter);
    }
}