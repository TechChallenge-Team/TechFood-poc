using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;
using TechFood.Domain.Enums;

namespace TechFood.Application.UseCases.Preparation.Queries;

public class GetMonitorPreparationsQuery : IRequest<IEnumerable<GetMonitorPreparationsQuery.Result>>
{
    public class Handler(IPreparationQueryProvider query) : IRequestHandler<GetMonitorPreparationsQuery, IEnumerable<Result>>
    {
        public Task<IEnumerable<Result>> Handle(GetMonitorPreparationsQuery request, CancellationToken cancellationToken)
            => query.GetMonitorPreparationsAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public PreparationStatusType Status { get; set; }
    }
}
