using System;
using System.Threading.Tasks;
using TechFood.Application.Common.Exceptions;
using TechFood.Application.Common.Resources;
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
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
    ) : IOrderUseCase
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IOrderNumberService _orderNumberService = orderNumberService;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    //TODO: caso venha um request customerId
    public async Task<CreateOrderResult> CreateAsync(CreateOrderRequest request)
    {
        var number = await _orderNumberService.GetAsync();

        //var customerId = _customerRepository.GetAsync();

        var order = new Order(number, request.CustomerId);

        var orderId = await _orderRepository.AddAsync(order);

        await _unitOfWork.CommitAsync();

        return new()
        {
            Id = orderId
        };
    }

    public async Task<AddOrderItemResult?> AddItemAsync(Guid orderId, AddOrderItemRequest data)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if(order == null)
        {
            return null;
        }

        var product = await _productRepository.GetByIdAsync(data.ProductId);

        if (product is null)
        {
            throw new NotFoundException(Exceptions.Order_ItemNotFound);
        }

        var item = new OrderItem(product.Id, 1, data.Quantity);

        order.AddItem(item);

        await _unitOfWork.CommitAsync();

        return new()
        {
            Id = item.Id
        };
    }

    public async Task<bool> RemoveItemAsync(Guid orderId, Guid itemId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
        {
            return false;
        }

        order.RemoveItem(itemId);

        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<CreatePaymentResult?> CreatePaymentAsync(Guid orderId, CreatePaymentRequest data)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
        {
            return null;
        }

        order.CreatePayment(data.Type);

        await _unitOfWork.CommitAsync();

        return new()
        {
            Id = order.Payment!.Id
        };
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
