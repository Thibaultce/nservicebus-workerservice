using NServiceBus.Logging;
using NServiceBus.Messages.Events;
using System.Threading.Tasks;

namespace NServiceBus.Shipping
{
    class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static readonly ILog _logger = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            _logger.Info($"Shipping has received OrderPlaced, OrderId = {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
