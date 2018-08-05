using System;
using System.Threading.Tasks;
using LanguaGo.Core.Domain;
using LanguaGo.Core.Repositories;
using LanguaGo.Infrastructure.DTO;

namespace LanguaGo.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
        
            if (user == null)
            {
                throw new Exception($"Invalid credentials.");
            }

            if (user.Password != password)
            {
                throw new Exception($"Invalid credentials.");
            }

            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDto()
            {
                Token = jwt.Token,
                Role = user.Role,
                Expires = jwt.Expires,
            };
        }

        public async Task RegisterAsync(Guid userId, string email, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }

            user = new User(userId, email, password, role);
            await _userRepository.AddAsync(user);
        }
    }
}