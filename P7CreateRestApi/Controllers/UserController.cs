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
        private readonly UserRepository _repository;
        private readonly IMapper<User, UserDTO> _mapper;

        public UserController(UserRepository repository, IMapper<User, UserDTO> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var users = _repository.FindAll();
            return Ok(users.Result.Select(u => _mapper.ToDTO(u)));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _repository.FindById(id);
            if (user == null) return NotFound();
            return Ok(_mapper.ToDTO(user));
        }

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] UserCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = dto.UserName,
                Fullname = dto.Fullname,
                Role = dto.Role
            };

            var result = await _repository.Add(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, _mapper.ToDTO(user));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _repository.FindById(id);
            if (user == null) return NotFound();

            user.UserName = dto.UserName;
            user.Fullname = dto.Fullname;
            user.Role = dto.Role;

            var result = await _repository.Update(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(_mapper.ToDTO(user));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _repository.FindById(id);
            if (user == null) return NotFound();

            var result = await _repository.Delete(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}