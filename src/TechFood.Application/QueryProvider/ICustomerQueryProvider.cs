using System.Threading.Tasks;
using TechFood.Application.UseCases.Customer.Queries;

namespace TechFood.Application.QueryProvider;

public interface ICustomerQueryProvider
{
    Task<GetCustomerByDocumentQuery.Result?> GetByDocumentAsync(GetCustomerByDocumentQuery query);
}
