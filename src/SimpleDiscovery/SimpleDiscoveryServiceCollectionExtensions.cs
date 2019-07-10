using SimpleDiscovery;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SimpleDiscoveryServiceCollectionExtensions
    {
        public static SimpleDiscoveryBuilder AddSimpleDiscovery(this IServiceCollection services)
        {
            return new SimpleDiscoveryBuilder(services);
        }
    }
}