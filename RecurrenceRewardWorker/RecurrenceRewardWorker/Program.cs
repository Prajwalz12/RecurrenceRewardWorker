using Extensions.Services;
using Serilog;
using System.Reflection;
using Utility;

namespace RecurrenceRewardWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .UseSerilog(configureLogger: (context, configuration) =>
                {
                    var userName = context.Configuration["ElasticConfiguration:User"];
                    var password = context.Configuration["ElasticConfiguration:Password"];
                    var programCode = context.Configuration["ElasticConfiguration:ProgramCode"];
                    var isMultiNode = context.Configuration["ElasticConfiguration:IsMultiNode"];
                    if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
                {
                    //ElasticsearchConfigurationWithConnectionSetting(context, configuration, programCode, userName, password);
                    if (isMultiNode == "Yes")
                    {
                        ElasticsearchConfigurationWithConnectionSettingByMultiNode(context, configuration, programCode, userName, password);
                    }
                    else
                    {
                        ElasticsearchConfigurationWithConnectionSettingBySingleNode(context, configuration, programCode, userName, password);
                    }
                }
                    else
                {
                    //ElasticsearchConfigurationWithoutConnectionSetting(context, configuration, programCode);
                    if (isMultiNode == "Yes")
                    {
                        ElasticsearchConfigurationWithoutConnectionSettingByMultiNode(context, configuration, programCode);
                    }
                    else
                    {
                        ElasticsearchConfigurationWithoutConnectionSettingBySingleNode(context, configuration, programCode);
                    }
                }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDomainServices(hostContext.Configuration);
                    services.AddHostedService<RewardWorker>();
                });
        
        #region NodeConfig
        private static void ElasticsearchConfigurationWithConnectionSettingByMultiNode(HostBuilderContext context, LoggerConfiguration configuration, string programCode, string userName, string password)
        {
            var uris = context.Configuration
                            .GetSection("ElasticConfiguration:Uris")
                            .GetChildren()
                            .Select(x => new Uri(x.Value))
                            .ToArray();
            configuration.Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(nodes: uris)
            {
                AutoRegisterTemplate = true,
                ModifyConnectionSettings = x => x.BasicAuthentication(userName, password),
                IndexFormat = $"{programCode}-{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(oldValue: ".", newValue: "-")}-{DateTime.UtcNow:yyyy-MM}week-{WeeKNumClass.WeekNum()}"
            }
            )
            .Enrich.WithProperty(name: "Environment", context.HostingEnvironment.EnvironmentName)
            .ReadFrom.Configuration(context.Configuration);
        }
        private static void ElasticsearchConfigurationWithoutConnectionSettingByMultiNode(HostBuilderContext context, LoggerConfiguration configuration, string programCode)
        {
            var uris = context.Configuration
                            .GetSection("ElasticConfiguration:Uris")
                            .GetChildren()
                            .Select(x => new Uri(x.Value))
                            .ToArray();
            configuration.Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(nodes: uris)
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{programCode}-{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(oldValue: ".", newValue: "-")}-{DateTime.UtcNow:yyyy-MM}week-{WeeKNumClass.WeekNum()}"
            }
            )
            .Enrich.WithProperty(name: "Environment", context.HostingEnvironment.EnvironmentName)
            .ReadFrom.Configuration(context.Configuration);
        }

        private static void ElasticsearchConfigurationWithConnectionSettingBySingleNode(HostBuilderContext context, LoggerConfiguration configuration, string programCode, string userName, string password)
        {
            //var uris = context.Configuration
            //                .GetSection("ElasticConfiguration:Uri")
            //                .GetChildren()
            //                .Select(x => new Uri(x.Value))
            //                .ToArray();
            configuration.Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                ModifyConnectionSettings = x => x.BasicAuthentication(userName, password),
                IndexFormat = $"{programCode}-{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(oldValue: ".", newValue: "-")}-{DateTime.UtcNow:yyyy-MM}week-{WeeKNumClass.WeekNum()}"
            }
            )
            .Enrich.WithProperty(name: "Environment", context.HostingEnvironment.EnvironmentName)
            .ReadFrom.Configuration(context.Configuration);
        }
        private static void ElasticsearchConfigurationWithoutConnectionSettingBySingleNode(HostBuilderContext context, LoggerConfiguration configuration, string programCode)
        {
            //var uris = context.Configuration
            //                .GetSection("ElasticConfiguration:Uri")
            //                .GetChildren()
            //                .Select(x => new Uri(x.Value))
            //                .ToArray();
            configuration.Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{programCode}-{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(oldValue: ".", newValue: "-")}-{DateTime.UtcNow:yyyy-MM}week-{WeeKNumClass.WeekNum()}"
            }
            )
            .Enrich.WithProperty(name: "Environment", context.HostingEnvironment.EnvironmentName)
            .ReadFrom.Configuration(context.Configuration);
        }
        #endregion
    }
}