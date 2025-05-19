using System.Threading.Tasks;
using TechFood.Application.Models.Auth;

namespace TechFood.Application.UseCases.Interfaces;

public interface IAuthUseCase
{
    Task<SignInResult> SignInAsync(SignInRequest request);
}
