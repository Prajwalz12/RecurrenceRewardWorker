using GlobalConfigurationModel = Domain.Models.GlobalConfigurationModel;

namespace Domain.Parsers
{
    public class CustomerReferralParser
    {
        private readonly GlobalConfigurationModel.GlobalConfiguration _globalConfiguration;
        public CustomerReferralParser(GlobalConfigurationModel.GlobalConfiguration globalConfiguration) 
        {
            _globalConfiguration = globalConfiguration;
        }        
    }
}
