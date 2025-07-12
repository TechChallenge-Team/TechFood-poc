using TechFoodClean.Application.Presenters;
using TechFoodClean.Common.DTO.Customer;

namespace TechFoodClean.Application.Interfaces.Controller
{
    public interface ICustomerController
    {
        Task<CustomerPresenter?> CreateCustomerAsync(CreateCustomerRequestDTO data);

        Task<CustomerPresenter?> GetByDocumentAsync(string documentValue);
    }
}
