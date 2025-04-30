using System.Threading.Tasks;
using TechFood.Application.Models.Customer;

namespace TechFood.Application.UseCases.Interfaces
{
    public interface ICustomerUseCase
    {
        Task<AddCustomerItemResult> AddItemAsync(CreateCustomerRequest data);
    }
}
