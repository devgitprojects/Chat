using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        public UsersController(ChatContext dataProvider)
        {
            DataProvider = dataProvider;
        }

        protected ChatContext DataProvider { get; private set; }

        // GET: <controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await DataProvider.Users.ToArrayAsync();
        }

        // GET <controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await DataProvider.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return new ObjectResult(user);
        }

        // POST <controller>
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            DataProvider.Users.Add(user);
            await DataProvider.SaveChangesAsync();
            return Ok(user);
        }

        // PUT <controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!DataProvider.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            DataProvider.Update(user);
            await DataProvider.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE <controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = DataProvider.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            DataProvider.Users.Remove(user);
            await DataProvider.SaveChangesAsync();
            return Ok(user);
        }
    }
}
