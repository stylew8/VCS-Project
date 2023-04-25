namespace ShopApi.Models.OrderLines
{
    public class OrderLineBaseModel
    {
        public OrderLineBaseModel(int orderId, int productId)
        {
            OrderId = orderId;
            ProductId = productId;
        }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
