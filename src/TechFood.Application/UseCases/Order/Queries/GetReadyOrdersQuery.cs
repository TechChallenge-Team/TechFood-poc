using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;

namespace TechFood.Application.UseCases.Order.Queries;

public class GetReadyOrdersQuery : IRequest<IEnumerable<GetReadyOrdersQuery.Result>>
{
    public class Handler(IOrderQueryProvider queries) : IRequestHandler<GetReadyOrdersQuery, IEnumerable<Result>>
    {
        public Task<IEnumerable<Result>> Handle(GetReadyOrdersQuery request, CancellationToken cancellationToken)
            => queries.GetReadyOrdersAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
