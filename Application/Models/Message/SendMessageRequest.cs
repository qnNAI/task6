

namespace Application.Models.Message;

public class SendMessageRequest {

    public string Subject { get; set; } = null!;
    public string Content { get; set; } = null!;

    public string Recipient { get; set; } = null!;
    public string Sender { get; set; } = null!;
}