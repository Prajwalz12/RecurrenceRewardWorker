using Confluent.Kafka;
using Domain.Parsers;
using Domain.Processors;
using Domain.Services;
using Domain.Services.Kafka;
using Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Extensions.Services
{
    public static class RegisterServiceExtension
    {        
        private const string DBServiceBaseAddress = @"Services:DBService:BaseAddress";        
        private const string MongoServiceBaseAddress = @"Services:MongoService:BaseAddress";
        private const string ApplicationJson = @"application/json";

        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICustomerEventService, CustomerEventService>();
            services.AddSingleton<ICustomerVersionService, CustomerVersionService>();
            services.AddSingleton<ICustomerSummaryService, CustomerSummaryService>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<IGroupCampaignTransactionService, GroupCampaignTransactionService>();
            services.AddSingleton<ITransactionRewardService, TransactionRewardService>();
            services.AddSingleton<IMissedTransactionService, MissedTransactionService>();
            services.AddSingleton<ICampaignService, CampaignService>();
            services.AddSingleton<ILoyaltyFraudManagementService, LoyaltyFraudManagementService>();
            services.AddSingleton<IOfferMapService, OfferMapService>();
            services.AddSingleton<ICumulativeTransactionService, CumulativeTransactionService>();
            services.AddSingleton<ICustomerTimelineService, CustomerTimelineService>();
            services.AddSingleton<IReferralRuleService, ReferralRuleService>();
            services.AddSingleton<IGlobalConfigurationService, GlobalConfigurationService>();
            services.AddSingleton<IRecurrenceRewardService, RecurrenceRewardService>();
            
            services.AddSingleton(_ => new WebUIDBContext(configuration["DatabaseSettings:MySql:WebUIDBConnectionString"]));
            services.AddSingleton(_ => new LPassOLTPDBContext(configuration["DatabaseSettings:MySql:LPassOLTPDBConnectionString"]));

            services.AddSingleton<WebUIDatabaseService>();
            services.AddSingleton<LPassOLTPDatabaseService>();

            services.AddSingleton<Processor>();
            services.AddSingleton<ProcessorService>();
            services.AddSingleton<AdditionalConditionProcessor>();
            //services.AddSingleton<QueueServiceHelper>();

            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            var producerConfig = new ProducerConfig();
            var consumerConfig = new ConsumerConfig();
            
            configuration.Bind("Producer", producerConfig);
            configuration.Bind("Consumer", consumerConfig);

            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;

            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddSingleton<ConsumerConfig>(consumerConfig);

            services.AddHttpClient<DBService.DBServiceClient>(ServiceOptions(configuration, DBServiceBaseAddress));
            services.AddHttpClient<MongoService.MongoServiceClient>(ServiceOptions(configuration, MongoServiceBaseAddress));

            services.AddSingleton<MessageProducer>();
            services.AddSingleton<TransactionMessageProducer<Null, string>>();
            services.AddSingleton<TransactionMessageProducer<string, string>>();
            services.AddSingleton<MessageQueueService>();

            return services;
        }
        static Action<System.Net.Http.HttpClient> ServiceOptions(IConfiguration configuration, string BaseAddress)
        {
            return c =>
            {
                c.BaseAddress = new Uri(configuration[BaseAddress]);
                c.DefaultRequestHeaders.Add("Accept", ApplicationJson);
            };
        }
    }
}
