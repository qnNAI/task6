using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace task6.SignalR;

public class UserIdProvider : IUserIdProvider {

    public virtual string GetUserId(HubConnectionContext context) {
        return context.User?.Identity?.Name;
    }
}