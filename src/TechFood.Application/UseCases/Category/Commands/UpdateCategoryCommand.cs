using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using TechFood.Application.Common.Attributes;
using TechFood.Application.Common.Resources;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Category.Commands
{
    public class UpdateCategoryCommand : IRequest<UpdateCategoryCommand.Result>
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
        public IFormFile File { get; set; } = null!;

        public class Handler(
            ICategoryRepository repo,
            IImageUrlResolver imageUrl,
            IImageStorageService imageStore)
                : IRequestHandler<UpdateCategoryCommand, Result>
        {
            public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await repo.GetByIdAsync(request.Id);

                if (category == null)
                {
                    throw new Common.Exceptions.ApplicationException(Exceptions.Category_CategoryNotFound);
                }

                var imageFileName = category.ImageFileName;

                if (request.File != null)
                {
                    imageFileName = imageUrl.CreateImageFileName(request.Name, request.File.ContentType);

                    await imageStore.SaveAsync(request.File.OpenReadStream(),
                                               imageFileName,
                                               nameof(Domain.Entities.Category));

                    await imageStore.DeleteAsync(category.ImageFileName, nameof(Domain.Entities.Category));
                }

                category.UpdateAsync(request.Name, imageFileName);

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
}
