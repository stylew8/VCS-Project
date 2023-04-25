namespace ShopApi.Models.Order
{
    public class UpdateOrderModel : OrderModel
    {
        public UpdateOrderModel(
            int id,
            int userId,
            bool isPaid)
                        : base(
                              id,
                              userId,
                              isPaid)
        {
        }
    }
}
