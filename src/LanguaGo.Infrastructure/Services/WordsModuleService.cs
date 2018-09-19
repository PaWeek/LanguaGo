using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LanguaGo.Core.Domain;
using LanguaGo.Core.Repositories;
using LanguaGo.Infrastructure.DTO;
using LanguaGo.Infrastructure.Extensions;

namespace LanguaGo.Infrastructure.Services
{
    public class WordsModuleService : IWordsModuleService
    {
        private readonly IWordsModuleRepository _wordsModuleRepository;
        private readonly IMapper _mapper;

        public WordsModuleService(IWordsModuleRepository wordsModuleRepository, IMapper mapper)
        {
            _wordsModuleRepository = wordsModuleRepository;
            _mapper = mapper;
        }

        public async Task<WordsModuleDetailsDto> GetAsync(Guid userId, string name)
        {
            var module = await _wordsModuleRepository.GetAsync(userId, name);

            return _mapper.Map<WordsModuleDetailsDto>(module);
        }

        public async Task<WordsModuleDetailsDto> GetAsync(Guid userId, Guid id)
        {
            var module = await _wordsModuleRepository.GetAsync(userId, id);

            return _mapper.Map<WordsModuleDetailsDto>(module);
        }

        public async Task<IEnumerable<WordsModuleDto>> BrowseAsync(Guid userId, string name = null)
        {
            var modules = await _wordsModuleRepository.BrowseAsync(userId, name);
            modules = modules.OrderBy(x => x.Name);
            return _mapper.Map<IEnumerable<WordsModuleDto>>(modules);
        }

        public async Task CreateAsync(Guid userId, Guid id, string name, string description)
        {
            var module = await _wordsModuleRepository.GetAsync(userId, id);

            if (module != null)
            {
                throw new Exception($"Module with this id: {id} already exists.");
            }

            module = new WordsModule(id, userId, name, description);
            await _wordsModuleRepository.AddAsync(module);
        }
        
        public async Task UpdateAsync(Guid userId, Guid id, string name, string description)
        {
            var module = await _wordsModuleRepository.GetAsync(userId, name);

            if (module != null)
            {
                throw new Exception($"Module with this name: {name} already exists.");
            }

            module = await _wordsModuleRepository.GetOrFailAsync(userId, id);

            module.SetName(name);
            module.SetDescription(description);
            
            await _wordsModuleRepository.UpdateAsync(module);
        }

        public async Task DeleteAsync(Guid userId, Guid id)
        {
            var module = await _wordsModuleRepository.GetOrFailAsync(userId, id);

            await _wordsModuleRepository.DeleteAsync(module);
        }

        public async Task AddTermAsync(Guid userId, string moduleName, Guid id, string word, string translation)
        {
            var module = await _wordsModuleRepository.GetAsync(userId, moduleName);
            var term = new Term(id, module.Id, word, translation);

            await _wordsModuleRepository.AddTermAsync(term);
        }
        
        public async Task DeleteTermAsync(Guid userId, string moduleName, Guid id)
        {
            var module = await _wordsModuleRepository.GetAsync(userId, moduleName);
            var term = await _wordsModuleRepository.GetTermAsync(module.Id, id);

            await _wordsModuleRepository.DeleteTermAsync(term);
        }
    }
}