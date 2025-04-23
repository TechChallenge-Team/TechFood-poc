using System;
using System.Threading.Tasks;
using TechFood.Application.Models.Order;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases
{
    internal class OrderUseCase(
        IOrderRepository orderRepo
        ) : IOrderUseCase
    {
        private readonly IOrderRepository _orderRepo = orderRepo;

        public async Task<CreateOrderResult> CreateAsync(CreateOrderRequest request)
        {
            var order = new Order(request.CustomerId!.Value);

            return new()
            {
                Id = await _orderRepo.CreateAsync(order)
            };
        }

        public async Task<AddOrderItemResult> AddItemAsync(Guid orderId, AddOrderItemRequest data)
        {
            var order = await _orderRepo.FindByIdAsync(orderId);

            //var product = await _orderRepo.GetProductByIdAsync(data.ProductId!.Value);

            var item = new OrderItem(data.ProductId!.Value, 1, data.Quantity);

            order.AddItem(item);

            await _orderRepo.UpdateAsync(order);

            return new()
            {
                Id = item.Id
            };
        }

        public async Task RemoveItemAsync(Guid orderId, Guid itemId)
        {
            var order = await _orderRepo.FindByIdAsync(orderId);

            order.RemoveItem(itemId);

            await _orderRepo.UpdateAsync(order);
        }

        public async Task<CreatePaymentResult> CreatePaymentAsync(Guid orderId, CreatePaymentRequest data)
        {
            var order = await _orderRepo.FindByIdAsync(orderId);

            order.CreatePayment(data.Type);

            await _orderRepo.UpdateAsync(order);

            return new()
            {
                Id = order.Payment!.Id
            };
        }

        public async Task PrepareAsync(Guid orderId)
        {
            var order = await _orderRepo.FindByIdAsync(orderId);

            order.Prepare();

            await _orderRepo.UpdateAsync(order);
        }

        public async Task FinishAsync(Guid orderId)
        {
            var order = await _orderRepo.FindByIdAsync(orderId);

            order.Finish();

            await _orderRepo.UpdateAsync(order);
        }
    }
}
