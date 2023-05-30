using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Services;
using Application.Models.Identity;
using Application.Models.User;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class MessageService : IMessageService {
    private readonly IApplicationDbContext _context;

    public MessageService(IApplicationDbContext context) {
        _context = context;
    }


    public async Task<SendMessageResponse> Add(SendMessageRequest request) {
        var recipientUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Recipient);        
        if (recipientUser is null) {
            return new SendMessageResponse { 
                Succeeded = false,
                Errors = new string[] {
                    "Invalid recipient user!"
                } 
            };
        }

        var senderUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Sender);
        if (senderUser is null) {
            return new SendMessageResponse { 
                Succeeded = false,
                Errors = new string[] {
                    "Invalid sender user!"
                } 
            };
        }

        _context.Messages.Add(new Message {
            Id = Guid.NewGuid().ToString(),
            Subject = subject,
            Content = content,
            SenderId = senderUser.Id,
            RecipientId = recipientUser.Id,
            SentTime = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();

        return new SendMessageResponse{ Succeeded = true };
    }
}
