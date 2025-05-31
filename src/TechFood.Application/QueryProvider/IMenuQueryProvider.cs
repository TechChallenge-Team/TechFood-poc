using System.Threading.Tasks;
using TechFood.Application.UseCases.Menu.Queries;

namespace TechFood.Application.QueryProvider;

public interface IMenuQueryProvider
{
    Task<GetMenuQuery.Result> GetAsync(GetMenuQuery query);
}
