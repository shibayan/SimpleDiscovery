using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.Management.ResourceGraph;
using Microsoft.Azure.Management.ResourceGraph.Models;
using Microsoft.Extensions.Options;
using Microsoft.Rest;

using Newtonsoft.Json.Linq;

using SimpleDiscovery.AzureResourceManager.Internal;

namespace SimpleDiscovery.AzureResourceManager
{
    internal class AzureResourceManagerServiceRegistry : IServiceRegistry
    {
        public AzureResourceManagerServiceRegistry(IOptions<AzureResourceManagerOptions> options)
        {
            _options = options.Value;

            Load();
        }

        private readonly AzureResourceManagerOptions _options;
        private Dictionary<string, string[]> _loadedResources;

        private const string DefaultTagName = "Registry";

        private static readonly Random _random = new Random();

        public string GetService(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (!_loadedResources.TryGetValue(serviceName, out var hostNames))
            {
                return null;
            }

            return "https://" + (hostNames.Length == 1 ? hostNames[0] : hostNames[_random.Next(hostNames.Length)]);
        }

        private void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        private async Task LoadAsync()
        {
            var tenantId = _options.TenantId ?? GetCurrentTenantId();

            var resourceGraphClient = new ResourceGraphClient(new TokenCredentials(new AppAuthenticationTokenProvider(tenantId)));

            var subscriptionId = _options.SubscriptionId ?? GetCurrentSubscriptionId();
            var resourceGroup = _options.ResourceGroup ?? GetCurrentResourceGroup();
            var tagName = _options.CustomTagName ?? DefaultTagName;

            var query = new QueryRequest
            {
                Subscriptions = new[] { subscriptionId },
                Query = $"where type =~ 'microsoft.web/sites' and resourceGroup =~ '{resourceGroup}' and not(isnull(tags['{tagName}'])) | project hostName = properties.defaultHostName, serviceName = tags['{tagName}']",
                Options = new QueryRequestOptions { ResultFormat = ResultFormat.ObjectArray }
            };

            var response = await resourceGraphClient.ResourcesAsync(query).ConfigureAwait(false);

            var resources = ((JToken)response.Data).ToObject<QueryResult[]>();

            var newResources = resources.GroupBy(x => x.ServiceName, x => x.HostName)
                                        .ToDictionary(x => x.Key, x => x.ToArray());

            Interlocked.Exchange(ref _loadedResources, newResources);
        }

        private static string GetCurrentTenantId() => Environment.GetEnvironmentVariable("DISCOVERY_TENANT_ID");

        private static string GetCurrentSubscriptionId() => Environment.GetEnvironmentVariable("WEBSITE_OWNER_NAME")?.Split('+')[0] ??
                                                            Environment.GetEnvironmentVariable("DISCOVERY_SUBSCRIPTION_ID");

        private static string GetCurrentResourceGroup() => Environment.GetEnvironmentVariable("WEBSITE_RESOURCE_GROUP") ??
                                                           Environment.GetEnvironmentVariable("DISCOVERY_RESOURCE_GROUP");
    }
}
