using pract18.Models;

namespace pract18.Services
{
    public interface IUserService
    {
        void Registration(string username, string password);
        bool CheckPassword(string username, string password);
        User? GetUser(string username);
    }
}
