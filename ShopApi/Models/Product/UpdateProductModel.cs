namespace ShopApi.Models.Product
{
    public class UpdateProductModel : ProductModel
    {
        public UpdateProductModel(
                            int id,
                            string name,
                            string description,
                            int quantity,
                            string category,
                            int price,
                            byte[] image)
                                        : base(
                                               id,
                                               name,
                                               description,
                                               quantity,
                                               category,
                                               price,
                                               image)
        {
        }
    }
}
