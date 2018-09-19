using System;

namespace LanguaGo.Infrastructure.DTO
{
    public class TermDto
    {
        public Guid Id { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
    }
}