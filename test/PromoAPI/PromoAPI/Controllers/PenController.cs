using CommonLibrary.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace PromoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenController : ControllerBase
    {
        private readonly IRepository<Pen> _penRepository;

        public PenController(IRepository<Pen> penRepository)
        {
            _penRepository = penRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pen>>> Get()
        {
            return Ok(await _penRepository.Get());
        }

        [HttpPost]
        public async Task<ActionResult<Pen>> Post(Pen pen)
        {
            return CreatedAtAction("Post", await _penRepository.Post(pen));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pen>> Put(int id, Pen pen)
        {
            var result = await _penRepository.Put(id, pen);
            if(result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(pen);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pen>> Delete(int id)
        {
            var result = await _penRepository.Delete(id);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(result);
        }
    }
}
