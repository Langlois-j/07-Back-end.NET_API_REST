using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper<User, UserDTO> _mapper;

        public UserController(IRepository<User> repository, IMapper<User, UserDTO> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Home()
        {
            var List = await _repository.FindAll();
            return Ok(List.Select(b => _mapper.ToDTO(b)));
        }



        [HttpGet]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] UserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var mapped = _mapper.ToEntity(dto);
            var created = await _repository.Add(mapped);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.ToDTO(created));
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> ShowUpdateFormAsync(int id)
        {
             User? user = await _repository.FindById(id);
            
            if (user == null)
                throw new ArgumentException("Invalid user Id:" + id);

            return Ok();
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO dto)

        {
                var mapped = _mapper.ToEntity(dto);
                var updated = await _repository.Update(id, mapped);
                if (updated == null) return NotFound();
                return Ok(_mapper.ToDTO(updated));
            }

        

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _repository.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet]
        [Route("/secure/article-details")]
        public async Task<ActionResult<List<User>>> GetAllUserArticles()
        {
            return Ok();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.FindById(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.ToDTO(entity));
        }
    }
}