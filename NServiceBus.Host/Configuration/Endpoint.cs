namespace NServiceBus.Host.Configuration
{
    public class Endpoint
    {
        /// <summary>
        ///     Name of the endpoint.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Assembly of the endpoint's handlers.
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        ///     List of routing endpoints.
        /// </summary>
        public Endpoint[] RoutingEndpoints { get; set; }
    }

}
