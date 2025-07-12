using TechFoodClean.Domain;
using TechFoodClean.Domain.Enums;

namespace TechFoodClean.Application.Interfaces.Gateway
{
    public interface ICustomerGateway
    {
        Task<Guid> CreateAsync(Customer customer);

        Task<Customer?> GetByDocumentAsync(DocumentType documentType, string documentValue);
    }
}
