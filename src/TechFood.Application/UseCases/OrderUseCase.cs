using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Common.Data;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.Order;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases
{
    internal class OrderUseCase(
        IOrderRepository orderRepo,
        IOrderNumberService orderNumberService,
        IServiceProvider serviceProvider
        ) : IOrderUseCase
    {
        private readonly IOrderRepository _orderRepo = orderRepo;
        private readonly IOrderNumberService _orderNumberService = orderNumberService;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task<CreateOrderResult> CreateAsync(CreateOrderRequest request)
        {
            var number = await _orderNumberService.GetNumberAsync();
            var order = new Order(number, request.CustomerId);

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
            var result = new CreatePaymentResult();

            var order = await _orderRepo.FindByIdAsync(orderId);

            var paymentService = _serviceProvider.GetRequiredKeyedService<IPaymentService>(data.Type);

            if (paymentService is IQrCodePaymentService qrCodePayment)
            {
                var payment = await qrCodePayment.GeneratePaymentAsync(
                    new(
                        "TOTEM01",
                        order.Id,
                        "TechFood - Order #" + order.Number,
                        order.TotalAmount,
                        order.Items.ToList().ConvertAll(i => new PaymentItem(
                            i.ProductId.ToString(),
                            i.Quantity,
                            "unit",
                            i.UnitPrice,
                            i.UnitPrice * i.Quantity))
                        ));

                order.CreatePayment(data.Type);

                result.QrCodeData = payment.QrCodeData;
            }

            await _orderRepo.UpdateAsync(order);

            result.Id = order.Payment!.Id;

            return result;
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
