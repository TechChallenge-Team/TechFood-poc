using TechFoodClean.Domain.Entities;
using TechFoodClean.Domain.Enums;

namespace TechFoodClean.Domain.Interfaces.Gateway
{
    public interface ICustomerGateway
    {
        Task<Guid> CreateAsync(Customer customer);

        Task<Customer?> GetByDocumentAsync(DocumentType documentType, string documentValue);
    }
}
