
namespace Application.Common.Contracts.Services;

public interface IMessageService {

    Task<SendMessageResponse> Add(SendMessageRequest request);
}