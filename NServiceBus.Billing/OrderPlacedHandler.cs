using NServiceBus.Logging;
using NServiceBus.Messages.Events;
using System.Threading.Tasks;

namespace NServiceBus.Billing
{
    class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static readonly ILog _logger = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            _logger.Info($"Billing has received OrderPlaced, OrderId = {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
