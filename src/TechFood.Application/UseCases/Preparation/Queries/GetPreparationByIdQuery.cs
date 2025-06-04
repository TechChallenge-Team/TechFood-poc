using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;
using TechFood.Domain.Enums;

namespace TechFood.Application.UseCases.Preparation.Queries;

public class GetPreparationByIdQuery(Guid id) : IRequest<GetPreparationByIdQuery.Result?>
{
    public Guid Id { get; set; } = id;

    public class Handler(IPreparationQueryProvider queries) : IRequestHandler<GetPreparationByIdQuery, Result?>
    {
        public Task<Result?> Handle(GetPreparationByIdQuery request, CancellationToken cancellationToken)
            => queries.GetByIdAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? ReadyAt { get; set; }

        public PreparationStatusType Status { get; set; }
    }
}
