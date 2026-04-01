
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

        public CurveController(IRepository<CurvePoint> curvePointRepository, IMapper<CurvePoint, CurveDTO> mapper)
        {
            _repository = curvePointRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Home()
        {
            var curve = await _repository.FindAll();
            return Ok(curve.Select(b => _mapper.ToDTO(b)));
        }

        //[HttpGet]
        //[Route("add")]
        //public async Task<IActionResult> AddCurvePoint([FromBody]CurvePoint curvePoint)
        //{
        //    return Ok();
        //}

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody]CurveDTO curvePointdto)
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var Cure = _mapper.ToEntity(curvePointdto);
                var created = await _repository.Add(Cure);
                return CreatedAtAction(nameof(GetById), new { id = created.CurveId }, _mapper.ToDTO(created));
            }
            //// TODO: check data valid and save to db, after saving return bid list
            //return Ok();
        }

        //[HttpPut]
        //[Route("update/{id}")]
        //public async Task<IActionResult> ShowUpdateForm(int id, [FromBody] CurveDTO curvePointdto)
        //{
        //    var CurvePoint = _mapper.ToEntity(curvePointdto);
        //    var updated = await _repository.Update(id, CurvePoint);
        //    if (updated == null) return NotFound();
        //    return Ok(_mapper.ToDTO(updated));


        //    //// TODO: get CurvePoint by Id and to model then show to the form
        //    //return Ok();
        //}

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurveDTO curvePointdto)
        {
            var CurvePoint = _mapper.ToEntity(curvePointdto);
            var updated = await _repository.Update(id, CurvePoint);
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
            var bidList = await _repository.FindById(id);
            if (bidList == null) return NotFound();
            return Ok(_mapper.ToDTO(bidList));
        }
    }
}