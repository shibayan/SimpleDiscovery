using System;

using Microsoft.Extensions.DependencyInjection.Extensions;

using SimpleDiscovery;
using SimpleDiscovery.AzureResourceManager;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureResourceManagerBuilderExtensions
    {
        public static void AddAzureResourceManager(this SimpleDiscoveryBuilder builder)
        {
            builder.AddAzureResourceManager(_ => { });
        }

        public static void AddAzureResourceManager(this SimpleDiscoveryBuilder builder, Action<AzureResourceManagerOptions> optionsAction)
        {
            builder.Services.Configure(optionsAction);

            builder.Services.TryAddSingleton<IServiceRegistry, AzureResourceManagerServiceRegistry>();
        }
    }
}
