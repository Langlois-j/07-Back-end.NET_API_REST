
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
                return CreatedAtAction(nameof(GetById), new { id = created.CurveId }, _mapper.ToDTO(created));
            }
            //// TODO: check data valid and save to db, after saving return bid list
            //return Ok();
        }


        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurveDTO dto)
        {
            var mapped = _mapper.ToEntity(dto);
            var updated = await _repository.Update(id, mapped);
            if (updated == null) return NotFound();
            return Ok(_mapper.ToDTO(updated));
            //// TODO: check required fields, if valid call service to update Curve and return Curve list
            //return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var result = await _repository.Delete(id);
            if (!result) return NotFound();
            return NoContent();
            //// TODO: Find Curve by Id and delete the Curve, return to Curve list
            //return Ok();
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