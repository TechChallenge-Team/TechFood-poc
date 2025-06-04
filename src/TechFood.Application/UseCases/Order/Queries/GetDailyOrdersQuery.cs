using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;
using TechFood.Domain.Enums;

namespace TechFood.Application.UseCases.Order.Queries;

public class GetDailyOrdersQuery : IRequest<IEnumerable<GetDailyOrdersQuery.Result>>
{
    public class Handler(IOrderQueryProvider queries) : IRequestHandler<GetDailyOrdersQuery, IEnumerable<Result>>
    {
        public Task<IEnumerable<Result>> Handle(GetDailyOrdersQuery request, CancellationToken cancellationToken)
            => queries.GetDailyOrdersAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatusType Status { get; set; }

        public List<OrderItem> Items { get; set; } = [];

        public class OrderItem
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string ImageUrl { get; set; } = null!;

            public decimal Price { get; set; }

            public int Quantity { get; set; }
        }
    }
}
