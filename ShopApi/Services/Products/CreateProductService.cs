using DataAccess;
using DataAccessNew.Models;
using DataAccessNew.Repositories;
using ShopApi.Models.Product;
using ShopApi.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShopApi.Services.Products
{
    public class CreateProductService
    {
        private readonly IRepository<Product> _productRepository;

        public CreateProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductModel> CallAsync(CreateProductModel model)
        {
            Validate(model);

            var productEntity = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Quantity = model.Quantity,
                Category = model.Category,
                Price = model.Price,
                ImageData = model.ImageData,
            };

            await _productRepository.CreateAsync(productEntity);

            return new ProductModel(
                productEntity.Id,
                productEntity.Name,
                productEntity.Description,
                productEntity.Quantity,
                productEntity.Category,
                productEntity.Price,
                productEntity.ImageData
            );
        }

        private void Validate(CreateProductModel model)
        {

            if (string.IsNullOrEmpty(model.Name))
            {
                var validationResult = new ValidationResult(
                    "Pavadinimas privalomas",
                    new[] { "Name" }
                );

                throw new ValidationException(validationResult, null, null);
            }

            if (string.IsNullOrEmpty(model.Description))
            {
                var validationResult = new ValidationResult(
                    "Aprasymas yra privalomas",
                    new[] { "Description" }
                );

                throw new ValidationException(validationResult, null, null);
            }
        }
    }
}
