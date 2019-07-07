using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace SimpleDiscovery.AzureAppConfiguration
{
    internal class AzureAppConfigurationServiceRegistry : IServiceRegistry
    {
        public AzureAppConfigurationServiceRegistry(AzureAppConfigurationOptions options)
        {
            _configuration = new ConfigurationBuilder()
                             .AddAzureAppConfiguration(options)
                             .Build();
        }

        private const string KeyPrefix = "Services";
        private readonly IConfiguration _configuration;

        public string GetService(string serviceName)
        {
            return _configuration[$"{KeyPrefix}:{serviceName}"];
        }
    }
}