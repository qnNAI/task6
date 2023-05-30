

namespace Application.Models.Message;

public class SendMessageResponse {

    public bool Succeeded { get; set; }

    public MessageDto? Message { get; set; }

    public IEnumerable<string>? Errors { get; set; }
}