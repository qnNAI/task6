using Application.Common.Contracts.Services;
using Application.Models.Message;
using Application.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace task6.Controllers {

    [Authorize]
    public class MessageController : Controller {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default) {
            var result = await _service.GetPageAsync(new GetPageRequest {
                Page = page,
                PageSize = pageSize,
                Recipient = HttpContext.User?.Identity?.Name ?? string.Empty
            }, cancellationToken);

            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }

            return PartialView("_Messages", result.Messages);
        }
    }
}
