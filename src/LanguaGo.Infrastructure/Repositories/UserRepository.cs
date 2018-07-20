using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguaGo.Core.Domain;
using LanguaGo.Core.Repositories;

namespace LanguaGo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IList<User> _users = new List<User>
        {
            new User(Guid.NewGuid(), "Andrzej@hehe.pl", "Zielony", "User", false),
            new User(Guid.NewGuid(), "Benek@hehe.pl", "123cztery", "User", false),
            new User(Guid.NewGuid(), "hej@hehe.pl", "321zero", "User", true)
        };

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.FirstOrDefault(u => u.Email == email));

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }
    }
}