using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace SimpleDiscovery.AzureAppConfiguration
{
    internal class AzureAppConfigurationServiceRegistry : IServiceRegistry
    {
        public AzureAppConfigurationServiceRegistry(string keyPrefix, AzureAppConfigurationOptions options)
        {
            _keyPrefix = keyPrefix;

            _configuration = new ConfigurationBuilder()
                             .AddAzureAppConfiguration(options)
                             .Build();
        }

        private readonly string _keyPrefix;
        private readonly IConfiguration _configuration;

        public string GetService(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            return _configuration[$"{_keyPrefix}:{serviceName}"];
        }
    }
}