using System;

namespace LanguaGo.Core.Domain
{
    public class User : Entity
    {
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; protected set; }
        public bool IsConfirmed { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {

        }

        public User(Guid id, string email, string password, string role)
        {
            Id = id;
            Email = email;
            Password = Encryption.Sha1encrypt(password);
            Role = role;
            IsConfirmed = false;
            CreatedAt = DateTime.UtcNow;
        }
    }
}