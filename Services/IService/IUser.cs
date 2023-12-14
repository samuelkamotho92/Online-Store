using Online_Store.Dtos;
using Online_Store.Models;

namespace Online_Store.Services.IService
{
    public interface IUser
    {
        Task<User> GetUserByEmail(string email);

        Task<string> RegisterUser(User user);

        Task<string> LoginUser();

        Task<string> ResetPassword();

        Task<string> LogOutUser();
    }
}
