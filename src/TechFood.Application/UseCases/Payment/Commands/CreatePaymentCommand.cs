using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Common.Data;
using TechFood.Application.Common.Resources;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Domain.Enums;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Payment.Commands;

public class CreatePaymentCommand : IRequest<CreatePaymentCommand.Result>
{
    [Required]
    public Guid OrderId { get; set; }

    public PaymentType Type { get; set; }

    public class Handler(
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository,
        IProductRepository productRepository,
        IServiceProvider serviceProvider)
            : IRequestHandler<CreatePaymentCommand, Result>
    {
        public async Task<Result> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                throw new Common.Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);
            }

            var payment = new Domain.Entities.Payment(request.OrderId, request.Type, order.Amount);
            var products = await productRepository.GetAllAsync();

            var result = new Result();

            var paymentService = serviceProvider.GetRequiredKeyedService<IPaymentService>(request.Type);

            if (request.Type == PaymentType.MercadoPago)
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
            else if (request.Type == PaymentType.CreditCard)
            {
                // TODO: Implement credit card payment
                throw new NotImplementedException("Credit card payment is not implemented yet.");
            }

            result.Id = await paymentRepository.AddAsync(payment);

            return result;
        }
    }

    public class Result
    {
        public Guid Id { get; set; }

        public string? QrCodeData { get; set; }
    }
}
