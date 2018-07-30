using System;
using System.Threading.Tasks;

namespace LanguaGo.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid userId, string emai, string password, string role = "user");
        Task LoginAsync(string email, string password);
    }
}