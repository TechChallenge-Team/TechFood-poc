using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.OrderMonitor;
using TechFood.Application.Models.Preparation;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Repositories;
using TechFood.Domain.Shared.Interfaces;
using TechFood.Domain.UoW;

namespace TechFood.Application.UseCases;

internal class PreparationUseCase(
    IPreparationRepository preparationRepository,
    IOrderRepository orderRepository,
    IReadOnlyQuery<GetPreparationMonitorResult> readOnlyQuery,
    IUnitOfWork unitOfWork) : IPreparationUseCase
{
    private readonly IPreparationRepository _preparationRepository = preparationRepository;
    private readonly IReadOnlyQuery<GetPreparationMonitorResult> _readOnlyQuery = readOnlyQuery;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<GetPreparationMonitorResult>> GetAllPreparationOrdersAsync()
    {
        return await _readOnlyQuery.GetAllAsync();
    }

    public async Task<IEnumerable<GetPreparationResult>> GetAllAsync()
    {
        var items = await _preparationRepository.GetAllAsync();

        return items.Select(item =>
        {
            return new GetPreparationResult
            {
                Id = item.Id,
                OrderId = item.OrderId,
                Status = item.Status,
                CreatedAt = item.CreatedAt,
                FinishedAt = item.FinishedAt,
                StartedAt = item.StartedAt,
                Number = item.Number
            };
        });
    }

    public async Task<GetPreparationResult> GetByIdAsync(Guid id)
    {
        var preparation = await _preparationRepository.GetByIdAsync(id);

        if (preparation is null)
        {
            throw new Common.Exceptions.ApplicationException("Preparation not found");
        }

        return new GetPreparationResult
        {
            Id = preparation.Id,
            OrderId = preparation.OrderId,
            Status = preparation.Status,
            CreatedAt = preparation.CreatedAt,
            FinishedAt = preparation.FinishedAt,
            StartedAt = preparation.StartedAt,
        };
    }

    public async Task StartAsync(Guid id)
    {
        var preparation = await _preparationRepository.GetByIdAsync(id);
        if (preparation is null)
        {
            throw new Common.Exceptions.ApplicationException("Preparation not found");
        }

        var order = await _orderRepository.GetByIdAsync(preparation.OrderId);
        if (order is null)
        {
            throw new Common.Exceptions.ApplicationException("Order not found");
        }

        preparation.Start();

        order.StartPreparation();

        await _unitOfWork.CommitAsync();
    }

    public async Task FinishAsync(Guid id)
    {
        var preparation = await _preparationRepository.GetByIdAsync(id);

        if (preparation is null)
        {
            throw new Common.Exceptions.ApplicationException("Preparation not found");
        }

        var order = await _orderRepository.GetByIdAsync(preparation.OrderId);
        if (order is null)
        {
            throw new Common.Exceptions.ApplicationException("Order not found");
        }

        preparation.Finish();

        order.FinishPreparation();

        await _unitOfWork.CommitAsync();
    }

    public async Task CancelAsync(Guid id)
    {
        var preparation = await _preparationRepository.GetByIdAsync(id);

        if (preparation is null)
        {
            throw new Common.Exceptions.ApplicationException("Preparation not found");
        }

        var order = await _orderRepository.GetByIdAsync(preparation.OrderId);
        if (order is null)
        {
            throw new Common.Exceptions.ApplicationException("Order not found");
        }

        preparation.Cancel();

        order.CancelPreparation();

        await _unitOfWork.CommitAsync();
    }

    public async Task<int> GetPreparationByOrderIdAsync(Guid orderId)
    {
        var preparation = await _preparationRepository.GetByOrderIdAsync(orderId);

        if (preparation is null)
            throw new Common.Exceptions.ApplicationException("Preparation not found");

        return preparation.Number;
    }
}
