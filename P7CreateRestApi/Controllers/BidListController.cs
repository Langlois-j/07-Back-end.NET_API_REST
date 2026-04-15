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
    public class BidListController : ControllerBase
    {
        private readonly IRepository<BidList> _repository;
        private readonly IMapper<BidList, BidListDTO> _mapper;

        public BidListController(IRepository<BidList> repository, IMapper<BidList, BidListDTO> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(Routes.List)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Home()
        {
            var List = await _repository.FindAll();
            return Ok(List.Select(b => _mapper.ToDTO(b)));
        }

      
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.FindById(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.ToDTO(entity));
        }

      
        [HttpPost]
        [Route(Routes.Validate)]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Validate([FromBody] BidListDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var mapped = _mapper.ToEntity(dto);
            var created = await _repository.Add(mapped);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.ToDTO(created));
        }

        [HttpPut]
        [Route(Routes.Update)]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListDTO bidListDTO)
        {
            var bidList = _mapper.ToEntity(bidListDTO);
            var updated = await _repository.Update(id, bidList);
            if (updated == null) return NotFound();
            return Ok(_mapper.ToDTO(updated));
        }

        
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var result = await _repository.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}