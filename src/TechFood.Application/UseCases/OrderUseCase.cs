using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.Order;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Domain.UoW;

namespace TechFood.Application.UseCases;

internal class OrderUseCase(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IOrderNumberService orderNumberService,
    IUnitOfWork unitOfWork
    ) : IOrderUseCase
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IOrderNumberService _orderNumberService = orderNumberService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    //TODO: caso venha um request customerId
    public async Task<CreateOrderResult> CreateAsync(CreateOrderRequest request)
    {
        var result = new CreateOrderResult();

        var products = await _productRepository.GetAllAsync();

        var items = request.Items
            .Select(i =>
            {
                var product = products.First(p => p!.Id == i.ProductId)!;
                return new OrderItem(product.Id, product.Price, i.Quantity);
            })
            .ToList();

        var number = await _orderNumberService.GetAsync();
        var order = new Order(number, request.CustomerId);

        foreach (var item in items)
        {
            order.AddItem(item);
        }

        await _orderRepository.AddAsync(order);

        await _unitOfWork.CommitAsync();

        result.Id = order.Id;
        result.Number = order.Number;

        return result;
    }

    public async Task<bool> PrepareAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
        {
            return false;
        }

        order.Prepare();

        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> FinishAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
        {
            return false;
        }

        order.Finish();

        await _unitOfWork.CommitAsync();

        return true;
    }
}
