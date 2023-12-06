using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class UserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> AddNewUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(string nickname)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Nickname.ToLower() == nickname.ToLower());
        }

        public async Task<bool> UserExists(string nickname)
        {
            bool exists = false;

            if (await _context.Users.AnyAsync(x => x.Nickname.ToLower() == nickname.ToLower()))
            {
                exists = true;
            }
            return exists;
        }
    }
}