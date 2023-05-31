using Application.Common.Contracts.Services;
using Application.Models.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace task6.SignalR;

[Authorize]
public class MessageHub : Hub {
    private readonly IMessageService _service;

    public MessageHub(IMessageService service) {
        _service = service;
    }

    public async Task Send(SendMessageRequest request) {
        var sender = Context.UserIdentifier ?? string.Empty;
        if (!_ValidateSendRequest(request)) {
            await Clients.User(sender).SendAsync("Error", "Request validation failed! Fill all fields.");
            return;
        }
        request.Sender = sender;
        var result = await _service.Add(request);

        if (!result.Succeeded)
        {
            await Clients.User(sender).SendAsync("Error", string.Join(" ", result.Errors!));
            return;
        }

        await Clients.User(sender).SendAsync("Success");

        result.Message!.SentTime = result.Message.SentTime.ToLocalTime();
        await Clients.User(request.Recipient).SendAsync("Receive", result.Message, result.Message!.SentTime.ToString());
    }

    public bool _ValidateSendRequest(SendMessageRequest request) {
        return !(string.IsNullOrEmpty(request.Subject) 
            || string.IsNullOrEmpty(request.Content) 
            || string.IsNullOrEmpty(request.Recipient));
    }
}