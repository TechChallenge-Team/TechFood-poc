using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using TechFood.Application.Common.Attributes;
using TechFood.Application.Common.Exceptions;
using TechFood.Application.Common.Resources;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Product.Commands;

public class CreateProductCommand : IRequest<CreateProductCommand.Result>
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
    public IFormFile File { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    public class Handler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IImageUrlResolver imageUrl,
        IImageStorageService imageStore)
            : IRequestHandler<CreateProductCommand, Result>
    {
        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByIdAsync(request.CategoryId);
            if (category is null)
            {
                throw new NotFoundException(Exceptions.Product_CaregoryNotFound);
            }

            var imageFileName = imageUrl.CreateImageFileName(request.Name, request.File.ContentType);

            await imageStore.SaveAsync(
                    request.File.OpenReadStream(),
                    imageFileName, nameof(Product));

            var product = new Domain.Entities.Product(
                request.Name,
                request.Description,
                request.CategoryId,
                imageFileName,
                request.Price);

            await productRepository.AddAsync(product);

            return new()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = category.Id,
                Description = product.Description,
                OutOfStock = product.OutOfStock,
                Price = product.Price,
                ImageUrl = imageUrl.BuildFilePath(nameof(Product).ToLower(), imageFileName)
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
