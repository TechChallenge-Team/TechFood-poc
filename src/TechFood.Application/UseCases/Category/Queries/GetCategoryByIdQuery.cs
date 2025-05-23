using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;

namespace TechFood.Application.UseCases.Category.Queries
{
    public class GetCategoryByIdQuery(Guid id) : IRequest<GetCategoryByIdQuery.Result?>
    {
        public Guid Id { get; set; } = id;

        public class Handler(ICategoryQueryProvider query) : IRequestHandler<GetCategoryByIdQuery, Result?>
        {
            public Task<Result?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
                => query.GetByIdAsync(request);
        }

        public class Result
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string ImageUrl { get; set; } = null!;
        }
    }
}
