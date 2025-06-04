using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;
using TechFood.Domain.Enums;

namespace TechFood.Application.UseCases.Preparation.Queries;

public class GetDailyPreparationsQuery : IRequest<IEnumerable<GetDailyPreparationsQuery.Result>>
{
    public class Handler(IPreparationQueryProvider queries) : IRequestHandler<GetDailyPreparationsQuery, IEnumerable<Result>>
    {
        public Task<IEnumerable<Result>> Handle(GetDailyPreparationsQuery request, CancellationToken cancellationToken)
            => queries.GetDailyPreparationsAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public decimal Amount { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? ReadyAt { get; set; }

        public PreparationStatusType Status { get; set; }

        public List<PreparationItem> Items { get; set; } = [];

        public class PreparationItem
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public int Quantity { get; set; }

            public string ImageUrl { get; set; } = null!;
        }
    }
}
