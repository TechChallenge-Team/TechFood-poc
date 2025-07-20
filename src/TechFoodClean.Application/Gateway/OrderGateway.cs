using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Mappers;
using TechFoodClean.Common.DTO.Enums;
using TechFoodClean.Common.Entities;
using TechFoodClean.Domain.Entities;
using TechFoodClean.Domain.Interfaces.Gateway;

namespace TechFoodClean.Application.Gateway
{
    public class OrderGateway : IOrderGateway
    {
        private readonly IUnitOfWorkDataSource _unitOfWork;
        private readonly IOrderDataSource _orderDataSource;

        public OrderGateway(IOrderDataSource orderDataSource,
                            IUnitOfWorkDataSource unitOfWork
                            )
        {
            _unitOfWork = unitOfWork;
            _orderDataSource = orderDataSource;
        }

        public async Task<Guid> AddAsync(Order order)
        {
            var orderDTO = new OrderDTO()
            {
                Amount = order.Amount,
                CustomerId = order.CustomerId,
                Discount = order.Discount,
                FinishedAt = order.FinishedAt,
                Historical = order.Historical.Select(h => new OrderHistoryDTO
                {
                    Id = h.Id,
                    OrderId = order.Id,
                    IsDeleted = h.IsDeleted,
                    CreatedAt = h.CreatedAt,
                    Status = (OrderStatusTypeDTO)h.Status
                }).ToList(),
                IsDeleted = order.IsDeleted,
                CreatedAt = order.CreatedAt,
                Status = (OrderStatusTypeDTO)order.Status,
                Items = order.Items.Select(i => new OrderItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            var result = await _orderDataSource.AddAsync(orderDTO);

            await _unitOfWork.CommitAsync();

            return result;
        }

        public Task<List<Order>> GetAllDoneAndInPreparationAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            var orderDTO = await _orderDataSource.GetByIdAsync(id);

            if (orderDTO is null)
            {
                return null;
            }

            return OrderMapper.ToDomain(orderDTO);
        }

        public async Task UpdateAsync(Order order)
        {
            var orderDTO = new OrderDTO()
            {
                Id = order.Id,
                Amount = order.Amount,
                CustomerId = order.CustomerId,
                Discount = order.Discount,
                FinishedAt = order.FinishedAt,
                Historical = order.Historical.Select(h => new OrderHistoryDTO
                {
                    Id = h.Id,
                    OrderId = order.Id,
                    IsDeleted = h.IsDeleted,
                    CreatedAt = h.CreatedAt,
                    Status = (OrderStatusTypeDTO)h.Status
                }).ToList(),
                IsDeleted = order.IsDeleted,
                CreatedAt = order.CreatedAt,
                Status = (OrderStatusTypeDTO)order.Status,
                Items = order.Items.Select(i => new OrderItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            await _orderDataSource.UpdateAsync(orderDTO);

            await _unitOfWork.CommitAsync();
        }
    }
}
