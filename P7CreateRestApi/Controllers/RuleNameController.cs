using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuleNameController : ControllerBase
    {
        private readonly IRepository<RuleName> _repository;
        private readonly IMapper<RuleName, RuleNameDTO> _mapper;

        public RuleNameController(IRepository<RuleName> repository, IMapper<RuleName, RuleNameDTO> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Home()
        {

            var List = await _repository.FindAll();
            return Ok(List.Select(b => _mapper.ToDTO(b)));
        }

        [HttpPost]
        [Route("validate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Validate([FromBody] RuleNameDTO dto)
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var mapped = _mapper.ToEntity(dto);
                var created = await _repository.Add(mapped);
                return CreatedAtAction(nameof(GetById), new { id = created.Id}, _mapper.ToDTO(created));
            }
        }


        [HttpPut]
        [Route("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRuleName(int id, [FromBody] RuleNameDTO dto)
        {
            var mapped = _mapper.ToEntity(dto);
            var updated = await _repository.Update(id, mapped);
            if (updated == null) return NotFound();
            return Ok(_mapper.ToDTO(updated));
        }



        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            var result = await _repository.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }


        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.FindById(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.ToDTO(entity));
        }
    }
}