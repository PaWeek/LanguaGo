using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguaGo.Core.Domain;

namespace LanguaGo.Core.Repositories
{
    public interface IWordsModuleRepository
    {
        Task<WordsModule> GetAsync(Guid userId, Guid id);
        Task<WordsModule> GetAsync(Guid userId, string name);
        Task<IEnumerable<WordsModule>> BrowseAsync(Guid userId, string name = "");
        Task AddAsync(WordsModule module);
        Task UpdateAsync(WordsModule module);
        Task DeleteAsync(WordsModule module);
        Task<Term> GetTermAsync(Guid moduleId, Guid id);
        Task AddTermAsync(Term term);
        Task DeleteTermAsync(Term term);
    }
}