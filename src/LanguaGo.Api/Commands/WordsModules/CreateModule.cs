using System;

namespace LanguaGo.Api.Commands.WordsModules
{
    public class CreateModule
    {
        public Guid ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}