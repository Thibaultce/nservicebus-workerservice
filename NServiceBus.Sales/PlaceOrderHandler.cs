using NServiceBus.Logging;
using NServiceBus.Messages.Commands;
using NServiceBus.Messages.Events;
using System.Threading.Tasks;

namespace NServiceBus.Sales
{
    class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static readonly ILog _logger = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            _logger.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };

            _logger.Info($"Publishing OrderPlaced, OrderId = {message.OrderId}");
            return context.Publish(orderPlaced);
        }
    }
}
