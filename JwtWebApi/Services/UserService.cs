using JwtWebApi.Context;
using JwtWebApi.Interfaces;
using JwtWebApi.Models;

namespace JwtWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int userId)
        {
            var userDelete = await GetUserById(userId);
            if (userDelete is null) return false;

            _context.Users.Remove(userDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool UserExists(string username, string email)
        {
            return _context.Users.Any(e => e.Username == username && e.Email == email);
        }
    }
}
