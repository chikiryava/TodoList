using System.ComponentModel.DataAnnotations;

namespace pract18.ViewModels
{
    public class AddPostViewModel
    {
        [Display(Name = "Название заметки")]
        [Required(ErrorMessage = "Укажите название заметки")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Текст заметки")]
        [Required(ErrorMessage = "Текст не может быть пустым")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; } = string.Empty;

        [Display(Name = "Изображение")]
        public IFormFile? Image { get; set; }
    }
}
