using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
      
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return Ok();
        }
    }
}