using URLShortener.Data.Entities;
using URLShortener.Business.DTOs;

namespace URLShortener.Business.Interfaces
{
    public interface IUserService
    {
        public Task<string> RegisterUserAsync(UserRequestDTO userRequestDto);
        public Task<string> LoginUserAsync(UserRequestDTO userRequestDto);
        public Task<User> GetUserByUsernameAsync(string email);
        public Task<User> GetUserByIdAsync(int id);
    }
}
