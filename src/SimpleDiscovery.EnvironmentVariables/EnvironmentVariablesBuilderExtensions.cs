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
            builder.AddEnvironmentVariables(_ => { });
        }

        public static void AddEnvironmentVariables(this SimpleDiscoveryBuilder builder, Action<EnvironmentVariablesOptions> optionsAction)
        {
            builder.Services.Configure(optionsAction);

            builder.Services.TryAddSingleton<IServiceRegistry, EnvironmentVariablesServiceRegistry>();
        }
    }
}
