using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NServiceBus.Messages.Commands;

namespace NServiceBus.ClientUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMessageSession _messageSession;
        private static int messagesSent;

        public IndexModel(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public string OrderId { get; private set; }
        public int MessagesSent { get; private set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var orderId = Guid.NewGuid().ToString().Substring(0, 8);

            var command = new PlaceOrder { OrderId = orderId };

            // Send the command
            await _messageSession.Send(command).ConfigureAwait(false);

            OrderId = orderId;
            MessagesSent = Interlocked.Increment(ref messagesSent);

            return Page();
        }
    }
}
