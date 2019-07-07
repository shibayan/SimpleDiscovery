using Microsoft.Extensions.DependencyInjection.Extensions;

using SimpleDiscovery;
using SimpleDiscovery.EnvironmentVariables;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EnvironmentVariablesBuilderExtensions
    {
        public static void AddEnvironmentVariables(this SimpleDiscoveryBuilder builder)
        {
            builder.Services.TryAddSingleton<IServiceRegistry, EnvironmentVariablesServiceRegistry>();
        }
    }
}
