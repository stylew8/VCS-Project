namespace ShopApi.Models.OrderLines
{
    public class UpdateOrderLineModel : OrderLineModel
    {
        public UpdateOrderLineModel(
            int id,
            int orderId,
            int productId)
                        : base(
                              id,
                              orderId,
                              productId)
        {
        }
    }
}
