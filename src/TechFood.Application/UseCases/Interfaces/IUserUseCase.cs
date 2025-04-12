using System.Threading.Tasks;
using TechFood.Application.Models;

namespace TechFood.Application.UseCases.Interfaces
{
    public interface IUserUseCase
    {
        Task<UserDto> GetUserByIdAsync(int id);
    }
}