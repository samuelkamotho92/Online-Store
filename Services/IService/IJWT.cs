using Online_Store.Models;

namespace Online_Store.Services.IService
{
    public interface IJWT
    {
       string  GenerateToken(User user);
    }
}
