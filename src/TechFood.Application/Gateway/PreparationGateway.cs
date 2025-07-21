using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO.Enums;
using TechFood.Common.Entities;
using TechFood.Domain.Entities;
using TechFood.Domain.Interfaces.Gateway;

namespace TechFood.Application.Gateway
{
    public class PreparationGateway : IPreparationGateway
    {
        private readonly IPreparationDataSource _preparationDataSource;
        private readonly IUnitOfWorkDataSource _unitOfWorkDataSource;

        public PreparationGateway(IUnitOfWorkDataSource unitOfWorkDataSource,
                                  IPreparationDataSource preparationDataSource)
        {
            _unitOfWorkDataSource = unitOfWorkDataSource;
            _preparationDataSource = preparationDataSource;
        }
        public async Task<Guid> AddAsync(Preparation preparation)
        {
            var preparationDto = new PreparationDTO
            {
                Id = preparation.Id,
                OrderId = preparation.OrderId,
                Number = preparation.Number,
                CreatedAt = preparation.CreatedAt,
                FinishedAt = preparation.FinishedAt,
                IsDeleted = preparation.IsDeleted,
                StartedAt = preparation.StartedAt,
                Status = (PreparationStatusTypeDTO)preparation.Status
            };


            var result = await _preparationDataSource.AddAsync(preparationDto);

            await _unitOfWorkDataSource.CommitAsync();

            return result;
        }

        public Task<IEnumerable<Preparation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Preparation?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Preparation?> GetByOrderIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
