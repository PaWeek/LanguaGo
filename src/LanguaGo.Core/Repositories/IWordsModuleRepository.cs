using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguaGo.Core.Domain;

namespace LanguaGo.Core.Repositories
{
    public interface IWordsModuleRepository
    {
        Task<IEnumerable<WordsModule>> GetAllAsync(Guid userId);
        Task<WordsModule> GetAsync(Guid userId, string name);
        Task AddAsync(WordsModule module);
        Task UpdateAsync(WordsModule module);
        Task DeleteAsync(WordsModule module);
        Task AddTermAsync(Term term);
        Task UpdateTermAsync(Term term);
        Task DeleteTermAsync(Term term);
    }
}