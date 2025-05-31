using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.QueryProvider;
using TechFood.Application.UseCases.Customer.Queries;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.QueryProviders;

internal class CustomerQueryProvider(TechFoodContext techFoodContext) : ICustomerQueryProvider
{
    public Task<GetCustomerByDocumentQuery.Result?> GetByDocumentAsync(GetCustomerByDocumentQuery query)
    {
        return techFoodContext.Customers
            .Where(c => c.Document.Type == query.DocumentType && c.Document.Value == query.DocumentValue)
            .Select(customer => new GetCustomerByDocumentQuery.Result
            {
                Id = customer.Id,
                DocumentType = customer.Document.Type,
                DocumentValue = customer.Document.Value,
                Name = customer.Name.FullName,
                Email = customer.Email,
                Phone = customer.Phone != null ? customer.Phone.Number : null
            })
            .FirstOrDefaultAsync();
    }
}
