using System.Security.Claims;
using Application.Common.Contracts.Services;
using Application.Models.User;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace task6.Controllers {

    [Authorize]
    public class UserController : Controller {

        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn() {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInRequest request) {
            var result = await _userService.SignInAsync(request);
            if (!result.Succeeded) {
                ModelState.AddModelError("", "Authentication failed!");
                return View(request);
            }

            await _SignInAsync(result);
            return RedirectToAction("", "Message");
        }

        [HttpPost]
        public async new Task<IActionResult> SignOut() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public async Task<JsonResult> SearchAutocomplete([FromQuery] string term) {
            var users = await _userService.GetUsersByPrefix(term);
            return Json(users.Select(x => x.Username).ToList());
        }

        private async Task _SignInAsync(AuthenticateResponse response) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, response.Id),
                new Claim(ClaimsIdentity.DefaultNameClaimType, response.Username),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
