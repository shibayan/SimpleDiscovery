using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Options;

namespace SimpleDiscovery.AzureAppConfiguration
{
    internal class AzureAppConfigurationServiceRegistry : IServiceRegistry
    {
        public AzureAppConfigurationServiceRegistry(IOptions<AzureAppConfigurationOptions> options)
        {
            _options = options.Value;

            _configuration = new ConfigurationBuilder()
                             .AddAzureAppConfiguration(_options.ConnectionString)
                             .Build();
        }

        private readonly AzureAppConfigurationOptions _options;
        private readonly IConfiguration _configuration;

        private const string DefaultPrefix = "Registry";

        public string GetService(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var prefix = _options.CustomPrefix ?? DefaultPrefix;

            return _configuration[$"{prefix}:{serviceName}"];
        }
    }
}