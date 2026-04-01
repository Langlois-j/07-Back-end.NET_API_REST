using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly IRepository<Trade> _repository;
        private readonly IMapper<Trade, TradeDTO> _mapper;

        public TradeController(IRepository<Trade> repository, IMapper<Trade, TradeDTO> mapper)
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
        public async Task<IActionResult> Validate([FromBody] TradeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var mapped = _mapper.ToEntity(dto);
            var created = await _repository.Add(mapped);
            return CreatedAtAction(nameof(GetById), new { id = created.TradeId }, _mapper.ToDTO(created));
            
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTrade(int id, [FromBody] TradeDTO dto)
        {
            var mapped = _mapper.ToEntity(dto);
            var updated = await _repository.Update(id, mapped);
            if (updated == null) return NotFound();
            return Ok(_mapper.ToDTO(updated));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTrade(int id)
        {
            var result = await _repository.Delete(id);
            if (!result) return NotFound();
            return NoContent();
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