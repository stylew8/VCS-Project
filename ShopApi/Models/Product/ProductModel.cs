using Microsoft.Extensions.Primitives;

namespace ShopApi.Models.Product
{
    public class ProductModel : ProductBaseModel
    {
        public ProductModel(
            int id,
            string name,
            string description,
            int quantity,
            string category,
            int price,
            byte[] image) 
                        : base(
                               name,
                               description,
                               quantity,
                               category,
                               price,
                               image)
        {
            Id = id;
        }
        public int Id { get; set; }        
    }
}
