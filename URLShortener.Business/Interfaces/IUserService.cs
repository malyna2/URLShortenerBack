using URLShortener.Data.Entities;

namespace URLShortener.Business.Interfaces
{
    public interface IUserService
    {
        public Task RegisterUserAsync(UserRequestDTO userRequestDto);
        public Task<string> LoginUserAsync(UserRequestDTO userRequestDto);
        public Task<User> GetUserByUsernameAsync(string email);
        public Task<User> GetUserByIdAsync(int id);
        public Task DeleteUserByIdAsync(int id);
    }
}
