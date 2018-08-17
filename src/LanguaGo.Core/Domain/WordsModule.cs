using System;
using System.Collections.Generic;

namespace LanguaGo.Core.Domain
{
    public class WordsModule : Entity
    {
        private ISet<Term> _terms = new HashSet<Term>();
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public IEnumerable<Term> Terms => _terms;

        protected WordsModule()
        {

        }

        public WordsModule(Guid id, Guid userId, string name, string description)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddTerm(Term term)
        {
            _terms.Add(term);
        }
    }
}