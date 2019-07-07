using Microsoft.Extensions.DependencyInjection;

namespace SimpleDiscovery
{
    public class SimpleDiscoveryBuilder
    {
        public SimpleDiscoveryBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}