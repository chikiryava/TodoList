using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using pract18.Models;
using pract18.Services;
using pract18.ViewModels;

using System.Security.Claims;

namespace pract18.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService userService;

        public LoginViewModel LoginViewModel { get; set; } = null!;

        public LoginModel(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult OnGet()
        {
            if(!User.Identity.IsAuthenticated)
                return Page();
            else
            {
                return RedirectToPage("index");
            }

        }

        public async Task<IActionResult> OnPost(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool isValidCredentials = userService.CheckPassword(loginViewModel.Username, loginViewModel.Password);

            if (!isValidCredentials)
            {
                ModelState.AddModelError("", "Ќеверное им€ пользовател€ или пароль");
                return Page();
            }

            User user = userService.GetUser(loginViewModel.Username)!;
            await LogIn(user);
            return Page();
        }

        private async Task LogIn(User user)
        {
            // набор утверждений о пользователе:
            // первый параметр - ключ, второй - значение
            List<Claim> userClaims = [
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, "Client")
            ];

            // создание "удостоверени€"
            var identity = new ClaimsIdentity(
                userClaims, // утверждени€ о пользователе
                CookieAuthenticationDefaults.AuthenticationScheme, // схема аутентификации (cookie)
                ClaimTypes.Name, // название ключа дл€ имени пользовател€
                ClaimTypes.Role); // название ключа дл€ роли
            var principal = new ClaimsPrincipal(identity);

            // вход в систему (используем HttpContext)
            await HttpContext.SignInAsync(principal);
        } 
    }
}
