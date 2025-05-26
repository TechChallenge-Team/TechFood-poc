using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.Common.Resources;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Product.Commands;

public class SetProductOutOfStockCommand : IRequest<SetProductOutOfStockCommand.Result>
{
    public Guid Id { get; set; }

    public bool OutOfStock { get; set; }

    public class Handler(IProductRepository repo, IImageUrlResolver imageUrl) : IRequestHandler<SetProductOutOfStockCommand, Result>
    {
        public async Task<Result> Handle(SetProductOutOfStockCommand request, CancellationToken cancellationToken)
        {
            var product = await repo.GetByIdAsync(request.Id);
            if (product is null)
            {
                throw new Common.Exceptions.ApplicationException(Exceptions.Product_ProductNotFound);
            }

            product.SetOutOfStock(request.OutOfStock);

            return new()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Description = product.Description,
                OutOfStock = product.OutOfStock,
                Price = product.Price,
                ImageUrl = imageUrl.BuildFilePath(nameof(Product).ToLower(), product.ImageFileName)
            };
        }
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
