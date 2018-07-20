using System.Threading.Tasks;
using LanguaGo.Core.Domain;

namespace LanguaGo.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}