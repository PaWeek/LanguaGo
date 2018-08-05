using System;
using System.Threading.Tasks;
using LanguaGo.Infrastructure.DTO;

namespace LanguaGo.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid userId, string emai, string password, string role = "user");
        Task<TokenDto> LoginAsync(string email, string password);
    }
}