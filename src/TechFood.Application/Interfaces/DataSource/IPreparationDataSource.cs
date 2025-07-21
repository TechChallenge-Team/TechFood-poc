using TechFood.Common.Entities;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IPreparationDataSource
    {
        Task<Guid> AddAsync(PreparationDTO preparation);

        Task<PreparationDTO?> GetByIdAsync(Guid id);

        Task<PreparationDTO?> GetByOrderIdAsync(Guid orderId);

        Task<IEnumerable<PreparationDTO>> GetAllAsync();
    }
}
