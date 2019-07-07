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
            var options = new AzureAppConfigurationOptions
            {
                ConnectionString = connectionString
            };

            builder.Services.TryAddSingleton<IServiceRegistry>(new AzureAppConfigurationServiceRegistry(options));
        }

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, Action<AzureAppConfigurationOptions> optionsAction)
        {
            var options = new AzureAppConfigurationOptions();

            optionsAction(options);

            builder.Services.TryAddSingleton<IServiceRegistry>(new AzureAppConfigurationServiceRegistry(options));
        }
    }
}