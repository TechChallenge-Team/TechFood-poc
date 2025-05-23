using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Order.Commands;

public class CreateOrderCommand : IRequest<CreateOrderCommand.Result>
{
    public Guid? CustomerId { get; set; }

    public List<Item> Items { get; set; } = [];

    public class Item
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }

    public class Handler(
        IOrderRepository orderRepo,
        IProductRepository productRepo,
        IOrderNumberService orderNumberService) : IRequestHandler<CreateOrderCommand, Result>
    {
        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var products = await productRepo.GetAllAsync();

            var items = request.Items
                .Select(i =>
                {
                    var product = products.First(p => p!.Id == i.ProductId)!;
                    return new OrderItem(product.Id, product.Price, i.Quantity);
                })
                .ToList();

            var number = await orderNumberService.GetAsync();
            var order = new Domain.Entities.Order(number, request.CustomerId);

            foreach (var item in items)
            {
                order.AddItem(item);
            }

            await orderRepo.AddAsync(order);

            return new()
            {
                Id = order.Id,
                Number = order.Number
            };
        }
    }

    public class Result
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
    }
}
