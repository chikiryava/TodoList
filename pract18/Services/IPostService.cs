using pract18.Models;

namespace pract18.Services
{
    public interface IPostsService
    {
        List<Post> GetPostsByUser(int userId);
        Post? FindPost(int postId);
        void AddPost(Post post);
        void UpdatePost(int postId, Post post);
        void DeletePost(Post post);
    }
}
