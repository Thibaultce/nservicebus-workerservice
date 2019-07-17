using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NServiceBus.Host.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NServiceBus.Host
{
    public class NServiceBusHost
    {
        private readonly ILogger<NServiceBusHost> _logger;
        private readonly NServiceBusOptions _nsbOptions;

        private readonly List<IEndpointInstance> _endpoints;

        public readonly string HostName;

        public NServiceBusHost(ILogger<NServiceBusHost> logger, IOptions<NServiceBusOptions> nsbOptions)
        {
            _logger = logger;
            _endpoints = new List<IEndpointInstance>();
            _nsbOptions = nsbOptions.Value;

            HostName = _nsbOptions.Hostname;
        }

        public async Task Start()
        {
            try
            {
                var endpoints = _nsbOptions.Endpoints;
                var assemblies = endpoints.Select(s => s.Assembly);

                foreach (var endpoint in endpoints)
                {
                    _endpoints.Add(await StartInstance(endpoint, assemblies).ConfigureAwait(false));
                }
            }
            catch (Exception ex)
            {
                FailFast("Failed to start.", ex);
            }
        }

        public async Task Stop()
        {
            try
            {
                foreach (var endpoint in _endpoints)
                    await endpoint?.Stop();
            }
            catch (Exception ex)
            {
                FailFast("Failed to stop correctly.", ex);
            }
        }

        Task<IEndpointInstance> StartInstance(Configuration.Endpoint endpoint, IEnumerable<string> assemblies)
        {
            var endpointConfiguration = new EndpointConfiguration(endpoint.Name);

            // Exclude the other endpoints .dlls and, by inference, include all other assemblies
            var scanner = endpointConfiguration.AssemblyScanner();
            var a = assemblies.Where(x => !string.Equals(x, endpoint.Assembly, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            scanner.ExcludeAssemblies(assemblies.Where(x => !string.Equals(x, endpoint.Assembly, StringComparison.CurrentCultureIgnoreCase)).ToArray());

            // Configure endpoint
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();

            endpointConfiguration.EnableInstallers();

            _logger.LogInformation($"Starts endpoint {endpoint.Name}");

            return Endpoint.Start(endpointConfiguration);
        }

        void FailFast(string message, Exception exception)
        {
            try
            {
                _logger.LogError(message, exception);
            }
            finally
            {
                Environment.FailFast(message, exception);
            }
        }
    }
}
