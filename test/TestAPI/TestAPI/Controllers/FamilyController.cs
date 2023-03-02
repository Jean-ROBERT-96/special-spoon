using CommonLibrary.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly IRepository<Family> _familyRepository;

        public FamilyController(IRepository<Family> familyRepository)
        {
            _familyRepository = familyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Family>>> Get()
        {
            return Ok(await _familyRepository.Get());
        }

        [HttpPost]
        public async Task<ActionResult<Family>> Post(Family family)
        {
            return CreatedAtAction("Post", await _familyRepository.Post(family));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Family>> Put(int id, Family family)
        {
            return Ok(await _familyRepository.Put(id, family));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Family>> Delete(int id)
        {
            return Ok(await _familyRepository.Delete(id));
        }
    }
}
