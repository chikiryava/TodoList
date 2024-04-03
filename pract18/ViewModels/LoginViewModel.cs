using System.ComponentModel.DataAnnotations;

namespace pract18.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)] // у input автоматически будет type="password"
        public string Password { get; set; } = string.Empty;
    }
}
