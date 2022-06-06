using System.Threading.Tasks;
using WebApplication_Cw4.Models;

namespace WebApplication_Cw4.Services
{
    public interface IAnimalDbService
    {
        Task<MethodResult> AddAnimalAsync(AnimalDTO animalDTO);
    }
}
