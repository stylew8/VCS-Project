namespace ShopApi.Models.Product
{
    public class CreateProductModel : ProductBaseModel
    {
        public CreateProductModel(
            string name,
            string description,
            int quantity,
            string category,
            int price,
            byte[] imageData)
                        : base(
                               name,
                               description,
                               quantity,
                               category,
                               price,
                               imageData)
        {
            
        }
    }
}
