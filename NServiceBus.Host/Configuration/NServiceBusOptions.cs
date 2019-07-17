namespace NServiceBus.Host.Configuration
{
    public class NServiceBusOptions
    {
        /// <summary>
        ///     Name of the host.
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        ///     List of endpoints.
        /// </summary>
        public Endpoint[] Endpoints { get; set; }
    }
}
