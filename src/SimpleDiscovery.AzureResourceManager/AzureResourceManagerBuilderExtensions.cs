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
            builder.AddAzureResourceManager(SimpleDiscoveryDefaults.DefaultTagName, _ => { });
        }

        public static void AddAzureResourceManager(this SimpleDiscoveryBuilder builder, string tagName)
        {
            builder.AddAzureResourceManager(tagName, _ => { });
        }

        public static void AddAzureResourceManager(this SimpleDiscoveryBuilder builder, Action<AzureResourceManagerOptions> optionsAction)
        {
            builder.AddAzureResourceManager(SimpleDiscoveryDefaults.DefaultTagName, optionsAction);
        }

        public static void AddAzureResourceManager(this SimpleDiscoveryBuilder builder, string tagName, Action<AzureResourceManagerOptions> optionsAction)
        {
            if (tagName == null)
            {
                throw new ArgumentNullException(nameof(tagName));
            }

            var options = new AzureResourceManagerOptions();

            optionsAction(options);

            builder.Services.TryAddSingleton<IServiceRegistry>(new AzureResourceManagerServiceRegistry(tagName, options));
        }
    }
}
