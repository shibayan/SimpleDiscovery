using System;

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
    }
}
