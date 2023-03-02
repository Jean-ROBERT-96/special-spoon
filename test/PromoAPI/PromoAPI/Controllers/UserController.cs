using CommonLibrary.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PromoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _userRepository.Get());
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            return CreatedAtAction("Post", await _userRepository.Post(user));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, User user)
        {
            var result = await _userRepository.Put(id, user);
            if(result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var result = await _userRepository.Delete(id);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(result);
        }
    }
}
