using CommonLibrary.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepository<Clients> _clientRepository;

        public ClientsController(IRepository<Clients> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Clients>>> Get()
        {
            return Ok(await _clientRepository.Get());
        }

        [HttpPost]
        public async Task<ActionResult<Clients>> Post(Clients clients)
        {
            return CreatedAtAction("Post", await _clientRepository.Post(clients));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Clients>> Put(int id, Clients clients)
        {
            return Ok(await _clientRepository.Put(id, clients));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Clients>> Delete(int id)
        {
            return Ok(await _clientRepository.Delete(id));
        }
    }
}
