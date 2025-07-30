using TechFoodClean.Common.DTO;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IPreparationDataSource
    {
        Task<Guid> AddAsync(PreparationDTO preparation);

        Task<PreparationDTO?> GetByIdAsync(Guid id);

        Task<PreparationDTO?> GetByOrderIdAsync(Guid orderId);

        Task<IEnumerable<PreparationDTO>> GetAllAsync();

        Task UpdateAsync(PreparationDTO preparation);

    }
}
