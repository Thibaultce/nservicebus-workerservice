namespace NServiceBus.Messages.Events
{
    public class OrderPlaced
    {
        /// <summary>
        ///     Identifier of the order.
        /// </summary>
        public string OrderId { get; set; }
    }
}
