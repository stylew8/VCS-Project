using DataAccessNew.Models;
using DataAccessNew.Repositories;
using ShopApi.Models.OrderLines;
using ShopApi.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Services.OrderLines
{
    public class CreateOrderLineService
    {
        private readonly IRepository<OrderLine> _orderLineRepository;

        public CreateOrderLineService(IRepository<OrderLine> orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
        }

        public async Task<OrderLineModel> CallAsync(CreateOrderLineModel model)
        {
            Validate(model);

            var productEntity = new OrderLine()
            {
                OrderId = model.OrderId,
                ProductId = model.ProductId
            };

            await _orderLineRepository.CreateAsync(productEntity);

            return new OrderLineModel(
                productEntity.Id,
                productEntity.OrderId,
                productEntity.ProductId
            );
        }

        private void Validate(CreateOrderLineModel model)
        {

            if (model.ProductId == null)
            {
                var validationResult = new ValidationResult(
                    "Pavadinimas privalomas",
                    new[] { "Name" }
                );

                throw new ValidationException(validationResult, null, null);
            }

            if (model.OrderId == null)
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
