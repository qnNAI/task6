using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Services;
using Application.Models.Message;
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

        var newMessageId = Guid.NewGuid().ToString();
        _context.Messages.Add(new Message {
            Id = newMessageId,
            Subject = request.Subject,
            Content = request.Content,
            SenderId = senderUser.Id,
            RecipientId = recipientUser.Id,
            SentTime = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
        var created = await _context.Messages.FindAsync(newMessageId);

        return new SendMessageResponse{ 
            Succeeded = true,
            Message = created?.Adapt<MessageDto>()
        };
    }

    public async Task<GetMessageResponse> GetPageAsync(GetPageRequest request, CancellationToken cancellationToken) {
        var recipient = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Recipient, cancellationToken);

        if (recipient is null) {
            return new GetMessageResponse {
                Succeeded = false,
                Errors = new string[] {
                    "Invalid recipient!"
                }
            };
        }

        var messages = await _context.Messages
            .Where(x => x.Recipient.Id == recipient.Id)
            .OrderByDescending(x => x.SentTime)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectToType<MessageDto>()
            .ToListAsync(cancellationToken);

        return new GetMessageResponse {
            Succeeded = true,
            Messages = messages
        };
    }
}
