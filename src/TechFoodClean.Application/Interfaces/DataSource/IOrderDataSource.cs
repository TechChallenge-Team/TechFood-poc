using TechFoodClean.Common.Entities;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IOrderDataSource
    {
        Task<Guid> AddAsync(OrderDTO order);
        Task<OrderDTO?> GetByIdAsync(Guid id);
        Task<List<OrderDTO>> GetAllDoneAndInPreparationAsync();
        Task UpdateAsync(OrderDTO order);
    }
}
