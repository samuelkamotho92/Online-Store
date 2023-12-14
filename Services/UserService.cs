using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.Services.IService;

namespace Online_Store.Services
{
    public class UserService : IUser
    {
        private readonly OnlineStoreDbContext _context;
        public UserService(OnlineStoreDbContext context) { 
        _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            //check if that email passed does exist in our database
            return await _context.Users.Where(x => x.email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
           
        }

        public async Task<string> LoginUser()
        {
            return "Logged in successfully";
           
        }

        public Task<string> LogOutUser()
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterUser(User user)
        {
            try
            {

                Console.WriteLine($"check user value {user}");
                   await  _context.Users.AddAsync(user);
                    _context.SaveChanges();
                return "successfully added user";
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return $"{e.InnerException}";
            }
        }

        public Task<string> ResetPassword()
        {
            throw new NotImplementedException();
        }
    }
}
