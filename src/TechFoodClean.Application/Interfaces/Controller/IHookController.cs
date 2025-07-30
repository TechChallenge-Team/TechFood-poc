using TechFoodClean.Common.DTO.Hook;

namespace TechFoodClean.Application.Interfaces.Controller
{
    public interface IHookController
    {
        Task InvokeAsync(HookRequestDTO request);
    }
}
