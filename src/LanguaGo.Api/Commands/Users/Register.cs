using System;

namespace LanguaGo.Api.Commands.Users
{
    public class Register
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}