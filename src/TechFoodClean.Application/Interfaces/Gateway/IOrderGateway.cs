using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.Gateway
{
    public interface IOrderGateway
    {
        Task<Guid> AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task<List<Order>> GetAllDoneAndInPreparationAsync();
        Task UpdateAsync(Order order);
    }
}
