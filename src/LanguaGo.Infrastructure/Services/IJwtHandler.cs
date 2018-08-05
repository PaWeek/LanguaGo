using System;
using LanguaGo.Infrastructure.DTO;

namespace LanguaGo.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}