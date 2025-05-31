using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using TechFood.Application.Common.Attributes;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Category.Commands;

public class CreateCategoryCommand : IRequest<CreateCategoryCommand.Result>
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
    public IFormFile File { get; set; } = null!;

    public class Handler(
        ICategoryRepository repo,
        IImageStorageService imageStore,
        IImageUrlResolver imageUrl)
            : IRequestHandler<CreateCategoryCommand, Result>
    {
        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var imageFileName = imageUrl.CreateImageFileName(request.Name, request.File.ContentType);

            var category = new Domain.Entities.Category(request.Name, imageFileName, 0);

            await imageStore.SaveAsync(request.File.OpenReadStream(),
                                       imageFileName,
                                       nameof(Category));

            await repo.AddAsync(category);

            return new()
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = imageUrl.BuildFilePath(nameof(Category).ToLower(), imageFileName)
            };
        }
    }

    public class Result
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
