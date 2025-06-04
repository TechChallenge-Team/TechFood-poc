using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;
using TechFood.Domain.Enums;

namespace TechFood.Application.UseCases.Preparation.Queries;

public class GetTrackingPreparationsQuery : IRequest<IEnumerable<GetTrackingPreparationsQuery.Result>>
{
    public class Handler(IPreparationQueryProvider queries) : IRequestHandler<GetTrackingPreparationsQuery, IEnumerable<Result>>
    {
        public Task<IEnumerable<Result>> Handle(GetTrackingPreparationsQuery request, CancellationToken cancellationToken)
            => queries.GetTrackingItemsAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public Guid OrderId { get; set; }

        public PreparationStatusType Status { get; set; }
    }
}
