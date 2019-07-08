using Microsoft.Extensions.DependencyInjection.Extensions;

using SimpleDiscovery;
using SimpleDiscovery.AzureResourceManager;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureResourceManagerBuilderExtensions
    {
        private const string DefaultTagName = "Registry";

        public static void AddAzureResourceManager(this SimpleDiscoveryBuilder builder)
        {
            builder.AddAzureResourceManager(DefaultTagName);
        }

        public static void AddAzureResourceManager(this SimpleDiscoveryBuilder builder, string tagName)
        {
            builder.Services.TryAddSingleton<IServiceRegistry>(new AzureResourceManagerServiceRegistry(tagName));
        }
    }
}
