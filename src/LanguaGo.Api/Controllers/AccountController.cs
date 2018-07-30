using System;
using System.Threading.Tasks;
using LanguaGo.Api.Commands.Users;
using LanguaGo.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguaGo.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Password, command.Role);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public Task<IActionResult> Post(Login command)
        {
            
        }
    }
}