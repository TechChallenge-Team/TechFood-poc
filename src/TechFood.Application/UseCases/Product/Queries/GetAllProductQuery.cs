using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;

namespace TechFood.Application.UseCases.Product.Queries;

public class GetAllProductQuery : IRequest<IEnumerable<GetAllProductQuery.Result>>
{
    public class Handler(IProductQueryProvider queries) : IRequestHandler<GetAllProductQuery, IEnumerable<Result>>
    {
        public Task<IEnumerable<Result>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            => queries.GetAllAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public Guid CategoryId { get; set; }

        public bool OutOfStock { get; set; }

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
