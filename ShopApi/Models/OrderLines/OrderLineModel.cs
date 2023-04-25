namespace ShopApi.Models.OrderLines
{
    public class OrderLineModel : OrderLineBaseModel
    {
        public OrderLineModel(
            int id,
            int orderId,
            int productId)
                : base(
                       orderId,
                       productId)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
