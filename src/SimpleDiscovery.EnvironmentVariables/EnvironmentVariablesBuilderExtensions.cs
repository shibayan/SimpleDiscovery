using System;

using Microsoft.Extensions.DependencyInjection.Extensions;

using SimpleDiscovery;
using SimpleDiscovery.EnvironmentVariables;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EnvironmentVariablesBuilderExtensions
    {
        public static void AddEnvironmentVariables(this SimpleDiscoveryBuilder builder)
        {
            builder.AddEnvironmentVariables(SimpleDiscoveryDefaults.DefaultKeyPrefix);
        }

        public static void AddEnvironmentVariables(this SimpleDiscoveryBuilder builder, string keyPrefix)
        {
            if (keyPrefix == null)
            {
                throw new ArgumentNullException(nameof(keyPrefix));
            }

            builder.Services.TryAddSingleton<IServiceRegistry>(new EnvironmentVariablesServiceRegistry(keyPrefix));
        }
    }
}
