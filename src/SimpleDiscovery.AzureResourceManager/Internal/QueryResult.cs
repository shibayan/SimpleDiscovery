using Newtonsoft.Json;

namespace SimpleDiscovery.AzureResourceManager.Internal
{
    internal class QueryResult
    {
        [JsonProperty("hostName")]
        public string HostName { get; set; }

        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }
    }
}