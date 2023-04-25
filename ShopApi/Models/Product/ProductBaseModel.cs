namespace ShopApi.Models.Product
{
    public class ProductBaseModel
    {
        public ProductBaseModel(
            string name,
            string description,
            int quantity,
            string category,
            int price,
            byte[] imageData)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
            Category = category;
            Price = price;
            ImageData = imageData;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public byte[] ImageData { get; set; }
    }
}
