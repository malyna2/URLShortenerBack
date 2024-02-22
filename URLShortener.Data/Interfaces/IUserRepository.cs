using URLShortener.Data.Entities;

namespace URLShortener.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(User user);
        public Task<User> GetUserByUsernameAsync(string email);
        public Task<User> GetUserByIdAsync(int id);
        public Task DeleteUserByIdAsync(int id);
    }
}
