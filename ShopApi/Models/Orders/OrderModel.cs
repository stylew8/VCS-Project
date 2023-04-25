namespace ShopApi.Models.Order
{
    public class OrderModel : OrderBaseModel
    {
        public OrderModel(
            int id,
            int userId,
            bool isPaid)
                        : base(
                               userId,
                               isPaid)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
