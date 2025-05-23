using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;

namespace TechFood.Application.UseCases.Product.Queries
{
    public class GetProductByIdQuery(Guid id) : IRequest<GetProductByIdQuery.Result?>
    {
        public Guid Id { get; set; } = id;

        public class Handler(IProductQueryProvider query) : IRequestHandler<GetProductByIdQuery, Result?>
        {
            public Task<Result?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
                => query.GetByIdAsync(request);
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
}
