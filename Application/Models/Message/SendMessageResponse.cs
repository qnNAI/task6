

namespace Application.Models.Message;

public class SendMessageResponse {

    public bool Succeeded { get; set; }

    public IEnumerable<string>? Errors { get; set; }
}