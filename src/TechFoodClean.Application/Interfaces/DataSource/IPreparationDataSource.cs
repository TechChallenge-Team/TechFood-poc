using TechFoodClean.Common.Entities;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IPreparationDataSource
    {
        Task<Guid> AddAsync(PreparationDTO preparation);

        Task<PreparationDTO?> GetByIdAsync(Guid id);

        Task<PreparationDTO?> GetByOrderIdAsync(Guid orderId);

        Task<IEnumerable<PreparationDTO>> GetAllAsync();
    }
}
