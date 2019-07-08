using System;

using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection.Extensions;

using SimpleDiscovery;
using SimpleDiscovery.AzureAppConfiguration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureAppConfigurationBuilderExtensions
    {
        private const string DefaultKeyPrefix = "Registry";

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, string connectionString)
        {
            builder.AddAzureAppConfiguration(DefaultKeyPrefix, options => options.Connect(connectionString));
        }

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, string keyPrefix, string connectionString)
        {
            builder.AddAzureAppConfiguration(keyPrefix, options => options.Connect(connectionString));
        }

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, Action<AzureAppConfigurationOptions> optionsAction)
        {
            builder.AddAzureAppConfiguration(DefaultKeyPrefix, optionsAction);
        }

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, string keyPrefix, Action<AzureAppConfigurationOptions> optionsAction)
        {
            var options = new AzureAppConfigurationOptions();

            optionsAction(options);

            builder.Services.TryAddSingleton<IServiceRegistry>(new AzureAppConfigurationServiceRegistry(keyPrefix, options));
        }
    }
}