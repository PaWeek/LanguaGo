using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguaGo.Infrastructure.DTO;

namespace LanguaGo.Infrastructure.Services
{
    public interface IWordsModuleService
    {
        Task<WordsModuleDetailsDto> GetAsync(Guid userId, string name);
        Task<WordsModuleDetailsDto> GetAsync(Guid userId, Guid id);
        Task<IEnumerable<WordsModuleDto>> BrowseAsync(Guid userId, string name = null);
        Task CreateAsync(Guid userId, Guid id, string name, string description);
        Task UpdateAsync(Guid userId, Guid id, string name, string description);
        Task DeleteAsync(Guid userId, Guid id);
        Task AddTermAsync(Guid userId, string moduleName, Guid id, string word, string translation);
        Task DeleteTermAsync(Guid userId, string moduleName, Guid id);
    }
}