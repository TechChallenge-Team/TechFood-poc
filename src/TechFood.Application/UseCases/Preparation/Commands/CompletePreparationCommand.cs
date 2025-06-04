using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.Common.Resources;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Preparation.Commands;

public class CompletePreparationCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; set; } = id;

    public class Handler(IPreparationRepository repo) : IRequestHandler<CompletePreparationCommand, Unit>
    {
        public async Task<Unit> Handle(CompletePreparationCommand request, CancellationToken cancellationToken)
        {
            var preparation = await repo.GetByIdAsync(request.Id);
            if (preparation == null)
            {
                throw new Common.Exceptions.ApplicationException(Exceptions.Preparation_PreparationNotFound);
            }

            preparation.Ready();

            return Unit.Value;
        }
    }
}
