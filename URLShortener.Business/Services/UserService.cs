using AutoMapper;
using URLShortener.Business.Interfaces;
using URLShortener.Business.DTOs;
using URLShortener.Data.Entities;
using URLShortener.Data.Interfaces;

namespace URLShortener.Business.Services
{
    public class UserService: IUserService
    {
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterUserAsync(UserRequestDTO userRequestDto)
        {
            if (await _userRepository.GetUserByUsernameAsync(userRequestDto.Username) != null)
            {
                throw new InvalidDataException("User already exists");
            }
            var user = _mapper.Map<User>(userRequestDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Password);
            await _userRepository.CreateUserAsync(user);

            var token = _tokenService.CreateToken(user);

            return token;
        }

        public async Task<string> LoginUserAsync(UserRequestDTO userRequestDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userRequestDto.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(userRequestDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid password");
            }

            var token = _tokenService.CreateToken(user);

            return token;
        }

        public async Task<User> GetUserByUsernameAsync(string email)
        {
            return await _userRepository.GetUserByUsernameAsync(email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}
