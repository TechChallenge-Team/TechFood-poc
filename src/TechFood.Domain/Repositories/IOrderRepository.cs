using System;
using System.Threading.Tasks;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Guid> CreateAsync(Order order);

        Task<Order> FindByIdAsync(Guid id);

        Task UpdateAsync(Order order);
    }
}
