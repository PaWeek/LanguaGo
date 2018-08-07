using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguaGo.Core.Domain;
using LanguaGo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LanguaGo.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        UserRepository repo = new UserRepository();

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            User user = await repo.GetAsync("paweek@gm.pl");
            return $"{user.Id}, {user.Email}, {user.Password}, {user.Role}, {user.IsConfirmed}, {user.CreatedAt}";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post()
        {
            var user = new User(new Guid("8b2d36ba-6b62-4664-aaaa-94ddd1559216"), "smail@email.pl", "password", "user");
            await repo.AddAsync(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task Delete()
        {
            var user = new User(Guid.NewGuid(), "email@email.pl", "password", "");
            await repo.DeleteAsync(user); 
        }
    }
}
