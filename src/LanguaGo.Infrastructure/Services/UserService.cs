using System;
using System.Threading.Tasks;
using LanguaGo.Core.Domain;
using LanguaGo.Core.Repositories;

namespace LanguaGo.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task LoginAsync(string email, string password)
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