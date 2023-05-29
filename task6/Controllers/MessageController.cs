using Application.Common.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace task6.Controllers {

    public class MessageController : Controller {

        [HttpGet]
        public IActionResult Index() {
            return View();
        }
        
    }
}
