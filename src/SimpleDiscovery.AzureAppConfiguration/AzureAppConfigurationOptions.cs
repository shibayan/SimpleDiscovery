namespace SimpleDiscovery.AzureAppConfiguration
{
    public class AzureAppConfigurationOptions
    {
        public string ConnectionString { get; set; }

        public string Endpoint { get; set; }

        public string LabelFilter { get; set; }

        public string CustomPrefix { get; set; }
    }
}
