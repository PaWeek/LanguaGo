using AutoMapper;
using LanguaGo.Core.Domain;
using LanguaGo.Infrastructure.DTO;

namespace LanguaGo.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<User, AccountDto>();
                cfg.CreateMap<WordsModule, WordsModuleDto>();
                cfg.CreateMap<WordsModule, WordsModuleDetailsDto>();
                cfg.CreateMap<Term, TermDto>();
            })
            .CreateMapper();
    }
}