using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Common.Data;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.Payment;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Repositories;
using TechFood.Domain.UoW;

namespace TechFood.Application.UseCases;

internal class PaymentUseCase(
    IUnitOfWork unitOfWork,
    IOrderRepository orderRepository,
    IPreparationRepository preparationRepository,
    IPaymentRepository paymentRepository,
    IProductRepository productRepository,
    IServiceProvider serviceProvider) : IPaymentUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IPreparationRepository _preparationRepository = preparationRepository;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<CreatePaymentResult?> CreateAsync(CreatePaymentRequest data)
    {
        var order = await _orderRepository.GetByIdAsync(data.OrderId!.Value);

        if (order == null)
        {
            return null;
        }

        var result = new CreatePaymentResult();

        var payment = new Payment(data.OrderId!.Value, data.Type, order.Amount);

        var paymentService = _serviceProvider.GetRequiredKeyedService<IPaymentService>(data.Type);

        var products = await _productRepository.GetAllAsync();

        if (data.Type == PaymentType.MercadoPago)
        {
            var paymentRequest = await paymentService.GenerateQrCodePaymentAsync(
                new(
                    "TOTEM01",
                    order.Id,
                    "TechFood - Order #" + order.Number,
                    order.Amount,
                    order.Items.ToList().ConvertAll(i => new PaymentItem(
                        products.FirstOrDefault(p => p.Id == i.ProductId)?.Name ?? "",
                        i.Quantity,
                        "unit",
                        i.UnitPrice,
                        i.UnitPrice * i.Quantity))
                    ));

            result.QrCodeData = paymentRequest.QrCodeData;
        }
        else if (data.Type == PaymentType.CreditCard)
        {
            // TODO: Implement credit card payment
            throw new NotImplementedException("Credit card payment is not implemented yet.");
        }

        order.CreatePayment();

        await _paymentRepository.AddAsync(payment);

        await _unitOfWork.CommitAsync();

        result.Id = payment.Id;

        return result;
    }

    public async Task ConfirmAsync(Guid id)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);
        if (payment == null)
        {
            throw new Common.Exceptions.ApplicationException("Payment not found.");
        }

        var order = await _orderRepository.GetByIdAsync(payment.OrderId);
        if (order == null)
        {
            throw new Common.Exceptions.ApplicationException("Order not found.");
        }

        payment.Confirm();

        order.ConfirmPayment();

        var preparation = new Preparation(payment.OrderId);

        await _preparationRepository.AddAsync(preparation);

        await _unitOfWork.CommitAsync();
    }
}
