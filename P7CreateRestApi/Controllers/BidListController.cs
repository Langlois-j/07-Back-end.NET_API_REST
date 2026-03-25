using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Dot.Net.WebApi.Mappers;
using P7CreateRestApi.DTOs;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IRepository<BidList> _bidListRepository;
        private readonly IMapper<BidList, BidListDTO> _mapper;

        public BidListController(IRepository<BidList> bidListRepository,IMapper<BidList, BidListDTO> mapper)
        {
            _bidListRepository = bidListRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Home()
        {
            var bidLists = await _bidListRepository.FindAll();
            return Ok(bidLists.Select(b => _mapper.ToDTO(b)));
        }

      
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bidList = await _bidListRepository.FindById(id);
            if (bidList == null) return NotFound();
            return Ok(_mapper.ToDTO(bidList));
        }

      
        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] BidListDTO bidListDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var bidList = _mapper.ToEntity(bidListDTO);
            var created = await _bidListRepository.Add(bidList);
            return CreatedAtAction(nameof(GetById), new { id = created.BidListId }, _mapper.ToDTO(created));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListDTO bidListDTO)
        {
            var bidList = _mapper.ToEntity(bidListDTO);
            var updated = await _bidListRepository.Update(id, bidList);
            if (updated == null) return NotFound();
            return Ok(_mapper.ToDTO(updated));
        }

        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var result = await _bidListRepository.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}