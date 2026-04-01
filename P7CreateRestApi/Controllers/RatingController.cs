using Microsoft.AspNetCore.Mvc;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.DTOs;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRepository<Rating> _repository;
        private readonly IMapper<Rating, RatingDTO> _mapper;

        public RatingController(IRepository<Rating> RatingRepository, IMapper<Rating, RatingDTO> mapper)
        {
            _repository = RatingRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Home()
        {
            var List = await _repository.FindAll();
            return Ok(List.Select(b => _mapper.ToDTO(b)));
            //// TODO: find all Rating, add to model
            //return Ok();
        }

        [HttpGet]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] RatingDTO dto)
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var mapped = _mapper.ToEntity(dto);
                var created = await _repository.Add(mapped);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.ToDTO(created));
            }
            //// TODO: check data valid and save to db, after saving return Rating list
            //return Ok();
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateRatingt(int id, [FromBody] RatingDTO dto)
        {
            var mapped = _mapper.ToEntity(dto);
            var updated = await _repository.Update(id, mapped);
            if (updated == null) return NotFound();
            return Ok(_mapper.ToDTO(updated));
            // // TODO: check required fields, if valid call service to update Rating and return Rating list
            //return Ok();
        }
    

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {

            var result = await _repository.Delete(id);
            if (!result) return NotFound();
            return NoContent();
            //// TODO: Find Rating by Id and delete the Rating, return to Rating list
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