using TechFoodClean.Common.DTO;
using TechFoodClean.Common.DTO.Enums;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface ICustomerDataSource
    {
        Task<Guid> CreateAsync(CustomerDTO customer);

        Task<CustomerDTO> GetByDocument(DocumentTypeDTO documentType, string documentValue);

        Task<CustomerDTO> GetByDocumentAsync(DocumentTypeDTO documentType, string documentValue);
    }
}
