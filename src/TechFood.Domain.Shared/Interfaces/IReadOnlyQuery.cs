using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechFood.Domain.Shared.Interfaces;

public interface IReadOnlyQuery<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
}
