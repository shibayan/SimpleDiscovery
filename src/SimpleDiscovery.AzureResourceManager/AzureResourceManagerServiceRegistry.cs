using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.Management.ResourceGraph;
using Microsoft.Azure.Management.ResourceGraph.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Rest;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SimpleDiscovery.AzureResourceManager
{
    internal class AzureResourceManagerServiceRegistry : IServiceRegistry
    {
        public AzureResourceManagerServiceRegistry(string tagName, AzureResourceManagerOptions options)
        {
            _tagName = tagName;
            _options = options;

            Load();
        }

        private readonly string _tagName;
        private readonly AzureResourceManagerOptions _options;
        private Dictionary<string, string[]> _loadedResources;

        public string GetService(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            return _loadedResources.TryGetValue(serviceName, out var hostNames) ? "https://" + hostNames[0] : null;
        }

        private void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        private async Task LoadAsync()
        {
            var resourceGraphClient = new ResourceGraphClient(new TokenCredentials(new AppAuthenticationTokenProvider(_options.TenantId)));

            var query = new QueryRequest
            {
                Subscriptions = new[] { _options.SubscriptionId },
                Query = $"where type =~ 'microsoft.web/sites' and not(isnull(tags['{_tagName}'])) | project hostName = properties.defaultHostName, serviceName = tags['{_tagName}']",
                Options = new QueryRequestOptions { ResultFormat = ResultFormat.ObjectArray }
            };

            var response = await resourceGraphClient.ResourcesAsync(query).ConfigureAwait(false);

            var resources = ((JToken)response.Data).ToObject<QueryResult[]>();

            var newResources = resources.GroupBy(x => x.ServiceName, x => x.HostName)
                                        .ToDictionary(x => x.Key, x => x.ToArray());

            Interlocked.Exchange(ref _loadedResources, newResources);
        }
    }

    internal class QueryResult
    {
        [JsonProperty("hostName")]
        public string HostName { get; set; }

        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }
    }

    internal class AppAuthenticationTokenProvider : ITokenProvider
    {
        public AppAuthenticationTokenProvider(string tenantId)
        {
            _tenantId = tenantId;
        }

        private readonly string _tenantId;

        public async Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync(CancellationToken cancellationToken)
        {
            var accessToken = await new AzureServiceTokenProvider().GetAccessTokenAsync("https://management.azure.com/", _tenantId, cancellationToken)
                                                                   .ConfigureAwait(false);

            return new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
