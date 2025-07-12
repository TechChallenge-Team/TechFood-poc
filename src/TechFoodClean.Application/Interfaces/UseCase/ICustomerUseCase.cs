using TechFoodClean.Common.DTO.Customer;
using TechFoodClean.Domain;

namespace TechFoodClean.Application.Interfaces.UseCase
{
    public interface ICustomerUseCase
    {
        Task<Customer?> CreateCustomerAsync(CreateCustomerRequestDTO data);

        Task<Customer?> GetByDocumentAsync(string documentType);
    }
}
