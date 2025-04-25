using System;
using System.Threading.Tasks;
using TechFood.Application.Models.Order;

namespace TechFood.Application.UseCases.Interfaces
{
    public interface IOrderUseCase
    {
        Task<CreateOrderResult> CreateAsync(CreateOrderRequest request);

        Task<AddOrderItemResult> AddItemAsync(Guid orderId, AddOrderItemRequest data);

        Task RemoveItemAsync(Guid orderId, Guid itemId);

        Task<CreatePaymentResult> CreatePaymentAsync(Guid orderId, CreatePaymentRequest data);

        Task PrepareAsync(Guid orderId);

        Task FinishAsync(Guid orderId);
    }
}
