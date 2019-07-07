using System;

namespace SimpleDiscovery.EnvironmentVariables
{
    public class EnvironmentVariablesServiceRegistry : IServiceRegistry
    {
        private const string KeyPrefix = "SERVICES";

        public string GetService(string serviceName)
        {
            return Environment.GetEnvironmentVariable($"{KeyPrefix}_{serviceName}");
        }
    }
}
