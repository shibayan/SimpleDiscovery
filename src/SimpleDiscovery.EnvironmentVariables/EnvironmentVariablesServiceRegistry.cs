using System;

using Microsoft.Extensions.Options;

namespace SimpleDiscovery.EnvironmentVariables
{
    internal class EnvironmentVariablesServiceRegistry : IServiceRegistry
    {
        public EnvironmentVariablesServiceRegistry(IOptions<EnvironmentVariablesOptions> options)
        {
            _options = options.Value;
        }

        private readonly EnvironmentVariablesOptions _options;

        private const string DefaultPrefix = "Registry";

        public string GetService(string serviceName)
        {
            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var prefix = _options.CustomPrefix ?? DefaultPrefix;

            return Environment.GetEnvironmentVariable($"{prefix}_{serviceName}");
        }
    }
}
