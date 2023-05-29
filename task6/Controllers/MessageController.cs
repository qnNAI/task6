using Application.Common.Contracts.Services;
using Application.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace task6.Controllers {

    [Authorize]
    public class MessageController : Controller {

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Test(MessageDto message) {

            return View("Index");
        }
        
    }
}
