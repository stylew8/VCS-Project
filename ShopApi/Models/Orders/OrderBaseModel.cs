namespace ShopApi.Models.Order
{
    public class OrderBaseModel
    {
        public OrderBaseModel(int userId, bool isPaid)
        {
            UserId = userId;
            IsPaid = isPaid;
        }

        public int UserId { get; set; }
        public bool IsPaid { get; set; }
    }
}
