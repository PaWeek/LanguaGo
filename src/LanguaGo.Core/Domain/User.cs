using System;
using System.Collections.Generic;

namespace LanguaGo.Core.Domain
{
    public class User : Entity
    {
        private ISet<WordsModule> _wordsModule = new HashSet<WordsModule>();
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; protected set; }
        public bool IsConfirmed { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public IEnumerable<WordsModule> Terms => _wordsModule;

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

        public void AddWordsModule(WordsModule module)
        {
            _wordsModule.Add(module);
        }
    }
}