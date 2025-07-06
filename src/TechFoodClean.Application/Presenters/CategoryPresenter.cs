using TechFoodClean.Application.Interfaces.Presenter;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Presenters
{
    public class CategoryPresenter
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public static CategoryPresenter Create(Category category, IImageUrlResolver imageUrlResolver)
        {
            return new CategoryPresenter
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = imageUrlResolver.BuildFilePath(nameof(Category).ToLower(), category.ImageFileName)
            };
        }

    }
}
