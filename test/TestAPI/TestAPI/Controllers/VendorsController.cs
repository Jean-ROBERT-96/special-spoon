using CommonLibrary.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IRepository<Vendors> _vendorsRepository;

        public VendorsController(IRepository<Vendors> vendorsRepository)
        {
            _vendorsRepository = vendorsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vendors>>> Get()
        {
            return Ok(await _vendorsRepository.Get());
        }

        [HttpPost]
        public async Task<ActionResult<Vendors>> Post(Vendors vendors)
        {
            return CreatedAtAction("Post", await _vendorsRepository.Post(vendors));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vendors>> Put(int id, Vendors vendors)
        {
            return Ok(await _vendorsRepository.Put(id, vendors));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendors>> Delete(int id)
        {
            return Ok(await _vendorsRepository.Delete(id));
        }
    }
}
