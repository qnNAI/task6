using Application.Common.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace task6.Controllers {

    public class UserController : Controller {

        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<JsonResult> SearchAutocomplete([FromQuery] string term) {
            var users = await _userService.GetUsersByPrefix(term);
            return Json(users.Select(x => x.Username).ToList());
        }
    }
}
