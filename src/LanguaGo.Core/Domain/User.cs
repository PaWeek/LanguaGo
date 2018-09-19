using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LanguaGo.Core.Domain
{
    public class User : Entity
    {
        private List<string> _roles = new List<string>
        {
            "user"
        };

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
            SetEmail(email);
            SetPassword(password);
            SetRole(role);
            IsConfirmed = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddWordsModule(WordsModule module)
        {
            _wordsModule.Add(module);
        }

        public void SetEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"Email can not be empty.");
            }

            Match match = regex.Match(email);
            
            if (!match.Success)
            {
                throw new Exception($"Email: {email} is not valid.");
            }

            Email = email;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"Password can not be empty.");
            }

            Password = Encryption.Sha1encrypt(password);
        }

        public void SetRole(string role)
        {
            if (!_roles.Contains(role))
            {
                throw new Exception($"Role can not be: {role}.");
            }

            Role = role;
        }
    }
}