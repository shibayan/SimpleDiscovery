using System;

using Microsoft.Extensions.DependencyInjection.Extensions;

using SimpleDiscovery;
using SimpleDiscovery.AzureAppConfiguration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureAppConfigurationBuilderExtensions
    {
        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder)
        {
            builder.AddAzureAppConfiguration(_ => { });
        }

        public static void AddAzureAppConfiguration(this SimpleDiscoveryBuilder builder, Action<AzureAppConfigurationOptions> optionsAction)
        {
            builder.Services.Configure(optionsAction);

            builder.Services.TryAddSingleton<IServiceRegistry, AzureAppConfigurationServiceRegistry>();
        }
    }
}