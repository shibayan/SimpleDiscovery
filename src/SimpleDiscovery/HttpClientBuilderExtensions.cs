using System;

using SimpleDiscovery;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder WithSimpleDiscovery(this IHttpClientBuilder builder)
        {
            builder.ConfigureHttpClient((provider, httpClient) =>
            {
                var registry = provider.GetRequiredService<IServiceRegistry>();

                var serviceEndpoint = registry.GetService(builder.Name);

                if (string.IsNullOrEmpty(serviceEndpoint))
                {
                    throw new InvalidOperationException($"The {builder.Name} service has not been registered.");
                }

                httpClient.BaseAddress = new Uri(serviceEndpoint);
            });

            return builder;
        }

        public static IHttpClientBuilder WithSimpleDiscovery(this IHttpClientBuilder builder, string serviceName)
        {
            builder.ConfigureHttpClient((provider, httpClient) =>
            {
                var registry = provider.GetRequiredService<IServiceRegistry>();

                var serviceEndpoint = registry.GetService(serviceName);

                if (string.IsNullOrEmpty(serviceEndpoint))
                {
                    throw new InvalidOperationException($"The {serviceName} service has not been registered.");
                }

                httpClient.BaseAddress = new Uri(serviceEndpoint);
            });

            return builder;
        }
    }
}