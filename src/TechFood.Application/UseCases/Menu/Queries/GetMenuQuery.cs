using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;

namespace TechFood.Application.UseCases.Menu.Queries
{
    public class GetMenuQuery : IRequest<GetMenuQuery.Result>
    {
        public class Handler(IMenuQueryProvider queries) : IRequestHandler<GetMenuQuery, Result>
        {
            public Task<Result> Handle(GetMenuQuery request, CancellationToken cancellationToken)
                => queries.GetAsync(request);
        }

        public class Result
        {
            public IEnumerable<Category> Categories { get; set; } = [];

            public class Category
            {
                public Guid Id { get; set; }

                public string Name { get; set; } = null!;

                public string ImageUrl { get; set; } = null!;

                public int SortOrder { get; set; }

                public List<Product> Products { get; set; } = [];
            }

            public class Product
            {
                public Guid Id { get; set; }

                public Guid CategoryId { get; set; }

                public string Name { get; set; } = null!;

                public string Description { get; set; } = null!;

                public decimal Price { get; set; }

                public string ImageUrl { get; set; } = null!;
            }
        }
    }
}
