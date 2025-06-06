using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;
using TechFood.Domain.Enums;

namespace TechFood.Application.UseCases.Order.Queries;

public class GetOrdersQuery : IRequest<IEnumerable<GetOrdersQuery.Result>>
{
    public class Handler(IOrderQueryProvider queries) : IRequestHandler<GetOrdersQuery, IEnumerable<Result>>
    {
        public Task<IEnumerable<Result>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
            => queries.GetOrdersAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderCustomer Customer { get; set; } = null!;

        public OrderStatusType Status { get; set; }

        public List<OrderItem> Items { get; set; } = [];

        public class OrderCustomer
        {
            public string Name { get; set; } = null!;

            public DateTime CreatedAt { get; set; }
        }

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
