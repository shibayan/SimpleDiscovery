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
                var resolver = provider.GetRequiredService<IServiceRegistry>();

                httpClient.BaseAddress = new Uri(resolver.GetService(builder.Name));
            });

            return builder;
        }

        public static IHttpClientBuilder WithSimpleDiscovery(this IHttpClientBuilder builder, string serviceName)
        {
            builder.ConfigureHttpClient((provider, httpClient) =>
            {
                var resolver = provider.GetRequiredService<IServiceRegistry>();

                httpClient.BaseAddress = new Uri(resolver.GetService(serviceName));
            });

            return builder;
        }
    }
}