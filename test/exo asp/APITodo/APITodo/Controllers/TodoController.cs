using APITodo.Data;
using APITodo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITodo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        DataContext dataRepo;

        public TodoController(DataContext dataRepo)
        {
            this.dataRepo = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tasks>>> Get()
        {
            var result = await dataRepo.Tasks1.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> Get(int id)
        {
            var item = await dataRepo.Tasks1.FindAsync(id);
            if (item == null)
                return BadRequest("Element non trouvé.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Tasks>> Post(Tasks model)
        {
            var result = dataRepo.Tasks1.Add(model);
            await dataRepo.SaveChangesAsync();
            return CreatedAtAction("POST", model);
        }

        [HttpPut]
        public async Task<ActionResult<Tasks>> Put(int id, Tasks tasks)
        {
            if (tasks.Id != id)
                BadRequest("La tâche n'existe pas.");

            dataRepo.Entry(tasks).State = EntityState.Modified;
            await dataRepo.SaveChangesAsync();

            return Ok(tasks);
        }

        [HttpDelete]
        public async Task<ActionResult<Tasks>> Delete(int id)
        {
            var item = await dataRepo.Tasks1.FindAsync(id);
            if (item == null)
                return BadRequest("Element non trouvé.");

            dataRepo.Tasks1.Remove(item);
            await dataRepo.SaveChangesAsync();

            return Ok(item);
        }
    }
}
