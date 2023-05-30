using Microsoft.AspNetCore.SignalR;

namespace task6.SignalR;

[Authorize]
public class MessageHub : Hub {
    private readonly IMessageService _service;

    public MessageHub(IMessageService service) {
        _service = service;
    }

    public async Task Send(string recipient, string subject, string content) {
        await _service.Add(new SendMessageRequest {
            Subject = subject,
            Content = content,
            Recipient = recipient,
            Sender = Context.NameIdentifier
        });

        await Clients.User(recipient).SendAsync("Receive", subject, content, sender.NameIdentifier);
    }
}