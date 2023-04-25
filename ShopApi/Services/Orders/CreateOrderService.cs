using DataAccessNew.Models;
using DataAccessNew.Repositories;
using ShopApi.Models.Order;
using ShopApi.Models.OrderLines;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Services.Orders
{
    public class CreateOrderService
    {
        private readonly IRepository<Order> _orderLineRepository;

        public CreateOrderService(IRepository<Order> orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
        }

        public async Task<OrderModel> CallAsync(CreateOrderModel model)
        {
            Validate(model);

            var productEntity = new Order()
            {
                UserId = model.UserId,
                IsPaid = model.IsPaid,
            };

            await _orderLineRepository.CreateAsync(productEntity);

            return new OrderModel(
                productEntity.Id,
                productEntity.UserId,
                productEntity.IsPaid
            );
        }

        private void Validate(CreateOrderModel model)
        {

            if (model.UserId == null)
            {
                var validationResult = new ValidationResult(
                    "userId privalomas",
                    new[] { "Name" }
                );

                throw new ValidationException(validationResult, null, null);
            }

        }
    }
}
