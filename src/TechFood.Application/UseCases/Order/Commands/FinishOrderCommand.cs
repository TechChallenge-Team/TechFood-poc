using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.Common.Resources;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Order.Commands;

public class FinishOrderCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; set; } = id;

    public class Handler(IOrderRepository repo) : IRequestHandler<FinishOrderCommand, Unit>
    {
        public async Task<Unit> Handle(FinishOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await repo.GetByIdAsync(request.Id);

            if (order == null)
            {
                throw new Common.Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);
            }

            order.Finish();

            return Unit.Value;
        }
    }
}
