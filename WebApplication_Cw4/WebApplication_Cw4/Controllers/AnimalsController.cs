using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Cw4.Models;
using WebApplication_Cw4.Services;

namespace WebApplication_Cw4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalDbService _animalDbService;

        public AnimalsController(IAnimalDbService animalsDbService)
        {
            _animalDbService = animalsDbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(AnimalDTO animal)
        {
            MethodResult result = await _animalDbService.AddAnimalAsync(animal);

            return StatusCode((int)result.StatusCode, result.Message);
        }
    }
}
