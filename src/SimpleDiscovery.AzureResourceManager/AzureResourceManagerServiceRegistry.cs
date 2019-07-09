using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.Management.ResourceGraph;
using Microsoft.Azure.Management.ResourceGraph.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Rest;

namespace SimpleDiscovery.AzureResourceManager
{
    internal class AzureResourceManagerServiceRegistry : IServiceRegistry
    {
        public AzureResourceManagerServiceRegistry(string tagName)
        {
            _tagName = tagName;
        }

        private readonly string _tagName;

        public string GetService(string serviceName)
        {
            throw new NotImplementedException();
        }

        private async Task LoadAsync()
        {
            var resourceGraphClient = new ResourceGraphClient(new TokenCredentials(new AppAuthenticationTokenProvider()));

            var query = new QueryRequest
            {
                Query = $"where not(isnull(tags['{_tagName}'])) | project id, tags['{_tagName}']"
            };

            var resources = await resourceGraphClient.ResourcesAsync(query);
        }
    }

    internal class AppAuthenticationTokenProvider : ITokenProvider
    {
        public async Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync(CancellationToken cancellationToken)
        {
            var accessToken = await new AzureServiceTokenProvider().GetAccessTokenAsync("https://management.azure.com/", cancellationToken: cancellationToken);

            return new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
