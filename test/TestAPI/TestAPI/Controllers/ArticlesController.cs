using CommonLibrary.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IRepository<Articles> _articlesRepository;

        public ArticlesController(IRepository<Articles> articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Articles>>> Get()
        {
            return Ok(await _articlesRepository.Get());
        }

        [HttpPost]
        public async Task<ActionResult<Articles>> Post(Articles articles)
        {
            return CreatedAtAction("Post", await _articlesRepository.Post(articles));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Articles>> Put(int id, Articles articles)
        {
            return Ok(await _articlesRepository.Put(id, articles));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Articles>> Delete(int id)
        {
            return Ok(await _articlesRepository.Delete(id));
        }
    }
}
