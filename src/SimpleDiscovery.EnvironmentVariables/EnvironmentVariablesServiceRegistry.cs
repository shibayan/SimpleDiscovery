using System;

namespace SimpleDiscovery.EnvironmentVariables
{
    internal class EnvironmentVariablesServiceRegistry : IServiceRegistry
    {
        public EnvironmentVariablesServiceRegistry(string keyPrefix)
        {
            _keyPrefix = keyPrefix;
        }

        private readonly string _keyPrefix;

        public string GetService(string serviceName)
        {
            return Environment.GetEnvironmentVariable($"{_keyPrefix}_{serviceName}");
        }
    }
}
