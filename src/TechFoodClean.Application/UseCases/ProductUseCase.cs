using TechFoodClean.Application.Interfaces.Gateway;
using TechFoodClean.Application.Interfaces.UseCase;
using TechFoodClean.Common.DTO.Product;
using TechFoodClean.Common.Exceptions;
using TechFoodClean.Common.Resources;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.UseCases
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductGateway _productGateway;
        private readonly ICategoryGateway _categoryGateway;
        public ProductUseCase(IProductGateway productGateway,
                              ICategoryGateway categoryGateway)
        {
            _productGateway = productGateway;
            _categoryGateway = categoryGateway;
        }

        public async Task<Product> AddAsync(CreateProductRequestDTO productDTO, string fileName)
        {
            var categoryDTO = await _categoryGateway.GetByIdAsync(productDTO.CategoryId)
                              ?? throw new NotFoundException(Exceptions.Product_CaregoryNotFound);

            var productEntity = new Product(null,
                                            productDTO.Name,
                                            productDTO.Description,
                                            categoryDTO.Id,
                                            fileName,
                                            false,
                                            productDTO.Price);

            await _productGateway.SaveImageAsync(productDTO.File, fileName);

            await _productGateway.AddAsync(productEntity);

            return productEntity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productGateway.GetByIdAsync(id);

            if (product != null)
            {
                await _productGateway.DeleteAsync(product);

                await _productGateway.DeleteImageAsync(product);

                return true;
            }

            return false;
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            return _productGateway.GetByIdAsync(id);
        }

        public Task<IEnumerable<Product>> ListAllAsync()
        {
            return _productGateway.GetAllAsync();
        }

        public async Task<Product?> UpdateAsync(Guid id, UpdateProductRequestDTO productDTO, string fileName)
        {
            var product = await _productGateway.GetByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            var imageFileName = string.IsNullOrEmpty(fileName) ? product.ImageFileName : fileName;

            if (productDTO.File != null)
            {
                await _productGateway.SaveImageAsync(productDTO.File, imageFileName);

                await _productGateway.DeleteImageAsync(product);
            }

            var category = await _categoryGateway.GetByIdAsync(productDTO.CategoryId)
                              ?? throw new NotFoundException(Exceptions.Product_CaregoryNotFound);

            product!.Update(
                productDTO.Name,
                productDTO.Description,
                imageFileName,
                productDTO.Price,
                category.Id);

            await _productGateway.UpdateAsync(product);

            return product;
        }
    }
}
