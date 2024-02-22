using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using URLShortener.Data.Entities;
using URLShortener.Data.Interfaces;

namespace URLShortener.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly URLShortenerDbContext _context;

        public UserRepository(URLShortenerDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == email);

            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        [Authorize("AdminPolicy")]
        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
