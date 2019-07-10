using System;

using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection.Extensions;

using SimpleDiscovery;
using SimpleDiscovery.AzureAppConfiguration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureAppConfigurationBuilderExtensions
    {
        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, string connectionString)
        {
            builder.AddAzureAppConfiguration(SimpleDiscoveryDefaults.DefaultKeyPrefix, options => options.Connect(connectionString));
        }

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, string keyPrefix, string connectionString)
        {
            builder.AddAzureAppConfiguration(keyPrefix, options => options.Connect(connectionString));
        }

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, Action<AzureAppConfigurationOptions> optionsAction)
        {
            builder.AddAzureAppConfiguration(SimpleDiscoveryDefaults.DefaultKeyPrefix, optionsAction);
        }

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, string keyPrefix, Action<AzureAppConfigurationOptions> optionsAction)
        {
            if (keyPrefix == null)
            {
                throw new ArgumentNullException(nameof(keyPrefix));
            }

            var options = new AzureAppConfigurationOptions();

            optionsAction(options);

            builder.Services.TryAddSingleton<IServiceRegistry>(new AzureAppConfigurationServiceRegistry(keyPrefix, options));
        }
    }
}