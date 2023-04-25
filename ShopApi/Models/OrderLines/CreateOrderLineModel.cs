using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopApi.Models.Order;
using ShopApi.Models.Product;

namespace ShopApi.Models.OrderLines
{
    public class CreateOrderLineModel : OrderLineBaseModel
    {
        public CreateOrderLineModel(
            int orderId,
            int productId)
                        : base(
                               orderId,
                               productId)
        {
            
        }
    }
}
