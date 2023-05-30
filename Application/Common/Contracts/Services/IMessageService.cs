
using Application.Models.Message;

namespace Application.Common.Contracts.Services;

public interface IMessageService {

    Task<SendMessageResponse> Add(SendMessageRequest request);
    Task<GetMessageResponse> GetPageAsync(GetPageRequest request, CancellationToken cancellationToken);
}