using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult Home()
        {
            return Ok();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddUser([FromBody]User user)
        {
            return Ok();
        }

        [HttpGet]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userRepository.Add(user);

            return Ok();
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> ShowUpdateFormAsync(int id)
        {
             User? user = await _userRepository.FindById(id);
            
            if (user == null)
                throw new ArgumentException("Invalid user Id:" + id);

            return Ok();
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            // TODO: check required fields, if valid call service to update Trade and return Trade list
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User? user = await _userRepository.FindById(id);
            
            if (user == null)
                throw new ArgumentException("Invalid user Id:" + id);

            return Ok();
        }

        [HttpGet]
        [Route("/secure/article-details")]
        public async Task<ActionResult<List<User>>> GetAllUserArticles()
        {
            return Ok();
        }
    }
}