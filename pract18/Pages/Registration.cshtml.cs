using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using pract18.Services;
using pract18.ViewModels;

namespace pract18.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly IUserService userService;

        public UserViewModel UserViewModel { get; set; } = new();

        public RegistrationModel(IUserService userService)
        {
            this.userService = userService;
        }

        public void OnGet()
        {
            // supports GET
        }
        public IActionResult OnPost(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contains = userService.GetUser(userViewModel.Username);
            if (contains is not null)
            {
                ModelState.AddModelError("Username", "Пользователь с таким именем уже существует");
                return Page();
            }

            try
            {
                userService.Registration(userViewModel.Username, userViewModel.Password);
                return RedirectToPage("/Index");
            }
            catch
            {
                ModelState.AddModelError("", "Произошла ошибка при сохранении. Попробуйте позже или обратитесь к администратору");
                return Page();
            }
        }
    }
}
