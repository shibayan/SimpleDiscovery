using Microsoft.Extensions.DependencyInjection.Extensions;

using SimpleDiscovery;
using SimpleDiscovery.EnvironmentVariables;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EnvironmentVariablesBuilderExtensions
    {
        private const string DefaultKeyPrefix = "Registry";

        public static void AddEnvironmentVariables(this SimpleDiscoveryBuilder builder)
        {
            builder.AddEnvironmentVariables(DefaultKeyPrefix);
        }

        public static void AddEnvironmentVariables(this SimpleDiscoveryBuilder builder, string keyPrefix)
        {
            builder.Services.TryAddSingleton<IServiceRegistry>(new EnvironmentVariablesServiceRegistry(keyPrefix));
        }
    }
}
