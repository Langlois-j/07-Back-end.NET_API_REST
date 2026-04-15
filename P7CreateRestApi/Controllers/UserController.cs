using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        [Route(Routes.List)]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.FindAll();

            var dtos = new List<UserDTO>();
            foreach (var user in users)
            {
                var dto = _mapper.ToDTO(user);
                var roles = await _repository.GetRolesAsync(user);
                dto.Role = roles.FirstOrDefault();
                dtos.Add(dto);
            }

            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _repository.FindById(id);
            if (user == null) return NotFound();

            var dto = _mapper.ToDTO(user);
            var roles = await _repository.GetRolesAsync(user);
            dto.Role = roles.FirstOrDefault();

            return Ok(dto);
        }

        [HttpPost]
        [Route(Routes.Validate)]
        [AllowAnonymous]
        public async Task<IActionResult> Validate([FromBody] UserCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = dto.UserName,
                Fullname = dto.Fullname,
            };

            var result = await _repository.Add(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _repository.AssignRoleAsync(user, dto.Role);
            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            var responseDto = _mapper.ToDTO(user);
            responseDto.Role = dto.Role;

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, responseDto);
        }

        [HttpPut]
        [Route(Routes.Update)]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _repository.FindById(id);
            if (user == null) return NotFound();

            user.UserName = dto.UserName;
            user.Fullname = dto.Fullname;

            var updateResult = await _repository.Update(user);
            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);

            if (!string.IsNullOrEmpty(dto.Role))
            {
                var roleResult = await _repository.AssignRoleAsync(user, dto.Role);
                if (!roleResult.Succeeded)
                    return BadRequest(roleResult.Errors);
            }

            var responseDto = _mapper.ToDTO(user);
            var roles = await _repository.GetRolesAsync(user);
            responseDto.Role = roles.FirstOrDefault();

            return Ok(responseDto);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
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