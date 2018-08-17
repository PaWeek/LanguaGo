using System;

namespace LanguaGo.Core.Domain
{
    public class Term : Entity
    {
        public Guid WordsModuleId { get; protected set; }
        public string Word { get; protected set; }
        public string Translation { get; protected set; }

        protected Term()
        {

        }

        public Term(Guid id, Guid wordsModuleId, string word, string translation)
        {
            Id = id;
            WordsModuleId = wordsModuleId;
            Word = word;
            Translation = translation;
        }
    }
}