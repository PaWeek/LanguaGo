using System.Collections.Generic;
using LanguaGo.Core.Domain;

namespace LanguaGo.Infrastructure.DTO
{
    public class WordsModuleDetailsDto
    {
        public string Name { get; set; }
        public IEnumerable<TermDto> Terms { get; set; }
    }
}