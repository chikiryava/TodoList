using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using pract18.Models;
using pract18.Services;
using pract18.ViewModels;

using System.Security.Claims;

namespace pract18.Pages
{
    public class AddPostModel : PageModel
    {
        private readonly IPostsService postsService;

        public AddPostViewModel AddPostViewModel { get; set; } = null!;

        public AddPostModel(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost(AddPostViewModel addPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // загрузка изображения
            string? url = null;

            if (addPostViewModel.Image is not null)
            {
                string ext = Path.GetExtension(addPostViewModel.Image.FileName);
                string filename = Path.GetRandomFileName() + ext;
                string filePath = Path.Combine(@"wwwroot/images", filename);
                using (FileStream file = new(filePath, FileMode.Create))
                {
                    await addPostViewModel.Image.CopyToAsync(file);
                }
                url = filename;
            }

            // получение из Claims идентификатора пользователя
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // создание и добавление поста
            Post post = new Post
            {
                Body = addPostViewModel.Body,
                Title = addPostViewModel.Title,
                CreatedDate = DateOnly.FromDateTime(DateTime.Today),
                ImageUrl = url,
                UserId = userId
            };

            postsService.AddPost(post);
            return RedirectToPage("Index");
        }
    }
}
