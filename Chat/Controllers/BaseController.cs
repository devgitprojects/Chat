using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntity> : Controller where TEntity : BaseModel
    {
        public BaseController(ChatContext dataProvider)
        {
            DataProvider = dataProvider;
        }

        protected ChatContext DataProvider { get; private set; }
        protected abstract DbSet<TEntity> Models { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await Models.ToArrayAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            TEntity model = await Models.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return new ObjectResult(model);
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Models.Add(model);
            await DataProvider.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Put(TEntity model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (!Models.Any(x => x.Id == model.Id))
            {
                return NotFound();
            }

            DataProvider.Update(model);
            await DataProvider.SaveChangesAsync();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            TEntity model = Models.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            Models.Remove(model);
            await DataProvider.SaveChangesAsync();
            return Ok(model);
        }

    }
}
