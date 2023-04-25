namespace ShopApi.Models.Order
{
    public class CreateOrderModel : OrderBaseModel
    {
        public CreateOrderModel(
            int userId,
            bool isPaid)
                        : base(
                               userId,
                               isPaid)
        {

        }
    }
}
