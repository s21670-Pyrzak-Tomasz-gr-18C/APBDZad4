using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Cw4.Controllers
{
    [Route("api")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        [HttpGet("animals")]
        public IActionResult getAnimals()
        {
            return Ok();
        }


    }
}
