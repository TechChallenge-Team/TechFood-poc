using System.Threading.Tasks;
using TechFood.Application.Models.Customer;

namespace TechFood.Application.UseCases.Interfaces;

public interface ICustomerUseCase
{
    Task<CreateCustomerResult> CreateCustomerAsync(CreateCustomerRequest data);
    
    Task<CustomerResponse> GetByDocumentAsync(string documentType, string documentValue);
}
