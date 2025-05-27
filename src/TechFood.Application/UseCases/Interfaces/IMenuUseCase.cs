using System.Threading.Tasks;
using TechFood.Application.Models.Menu;

namespace TechFood.Application.UseCases.Interfaces;

public interface IMenuUseCase
{
    Task<GetMenuResult> GetAsync();
}
