using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;

namespace TechFood.Application.UseCases.Category.Queries;

public class GetAllCategoryQuery : IRequest<IEnumerable<GetAllCategoryQuery.Result>>
{
    public class Handler(ICategoryQueryProvider queries) : IRequestHandler<GetAllCategoryQuery, IEnumerable<Result>>
    {
        public Task<IEnumerable<Result>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            => queries.GetAllAsync(request);
    }

    public class Result
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
