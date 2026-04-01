
using Microsoft.AspNetCore.Mvc;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.DTOs;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurveController : ControllerBase
    {
       
        private readonly IRepository<CurvePoint> _repository;
        private readonly IMapper<CurvePoint, CurveDTO> _mapper;

        public CurveController(IRepository<CurvePoint> repository, IMapper<CurvePoint, CurveDTO> mapper)
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

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody]CurveDTO dto)
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var mapped = _mapper.ToEntity(dto);
                var created = await _repository.Add(mapped);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.ToDTO(created));
            }

        }


        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurveDTO dto)
        {
            var mapped = _mapper.ToEntity(dto);
            var updated = await _repository.Update(id, mapped);
            if (updated == null) return NotFound();
            return Ok(_mapper.ToDTO(updated));

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
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