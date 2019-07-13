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
                             .AddAzureAppConfiguration(o =>
                             {
                                 if (_options.ConnectionString != null)
                                 {
                                     o.Connect(_options.ConnectionString);
                                 }
                                 else if (_options.Endpoint != null)
                                 {
                                     o.ConnectWithManagedIdentity(_options.Endpoint);
                                 }
                                 else
                                 {
                                     throw new InvalidOperationException("ConnectionString and Endpoint have not been configured.");
                                 }

                                 if (_options.LabelFilter != null)
                                 {
                                     o.Use(KeyFilter.Any, _options.LabelFilter);
                                 }
                             })
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