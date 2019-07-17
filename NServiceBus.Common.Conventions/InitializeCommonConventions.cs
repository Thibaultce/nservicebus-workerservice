namespace NServiceBus.Common.Conventions
{
    public class InitializeCommonConventions : INeedInitialization
    {
        public void Customize(EndpointConfiguration configuration)
        {
            configuration.Conventions()
                .DefiningCommandsAs(t =>
                    t != null && t.Namespace != null
                    && t.Namespace.EndsWith(".Commands"))
                .DefiningEventsAs(t =>
                    t != null && t.Namespace != null
                    && t.Namespace.EndsWith(".Events"));
        }
    }

}
