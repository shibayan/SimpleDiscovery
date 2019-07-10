using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Rest;

namespace SimpleDiscovery.AzureResourceManager.Internal
{
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