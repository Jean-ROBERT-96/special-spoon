using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Ajouter>>> Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Ajouter>> Post()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Ajouter>> Put()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<Ajouter>> Delete()
        {
            return Ok();
        }
    }
}
