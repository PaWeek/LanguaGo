using System;
using System.Threading.Tasks;
using LanguaGo.Api.Commands.WordsModules;
using LanguaGo.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguaGo.Api.Controllers
{
    [Route("[controller]")]
    public class WordsModulesController : ApiControllerBase
    {
        private readonly IWordsModuleService _wordsModuleService;

        public WordsModulesController(IWordsModuleService wordsModuleService)
        {
            _wordsModuleService = wordsModuleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var modules = await _wordsModuleService.BrowseAsync(UserId, name);

            return Json(modules);
        }

        [HttpGet("{moduleId}")]
        public async Task<IActionResult> Get(Guid moduleId)
        {
            var modules = await _wordsModuleService.GetAsync(UserId, moduleId);

            if (modules == null)
            {
                return NotFound();
            }

            return Json(modules);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateModule command)
        {
            command.ModuleId = Guid.NewGuid();
            await _wordsModuleService.CreateAsync(UserId, command.ModuleId, command.Name, command.Description);

            return Created($"/wordsmodules/{command.ModuleId}", null);
        }

        [HttpPut("{moduleId}")]
        public async Task<IActionResult> Put(Guid moduleId, [FromBody]UpdateModule command)
        {
            await _wordsModuleService.UpdateAsync(UserId, moduleId, command.Name, command.Description);

            return NoContent();
        }

        [HttpDelete("{moduleId}")]
        public async Task<IActionResult> Put(Guid moduleId)
        {
            await _wordsModuleService.DeleteAsync(UserId, moduleId);
            
            return NoContent();
        }
    }
}