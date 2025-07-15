using TechFoodClean.Common.DTO.Customer;

namespace TechFoodClean.Domain.Interfaces.UseCase
{
    public interface ICustomerUseCase
    {
        Task<Customer?> CreateCustomerAsync(CreateCustomerRequestDTO data);

        Task<Customer?> GetByDocumentAsync(string documentType);
    }
}
