namespace projRESTfulApiFitConnect.DTO.Product
{
    public class AddProductDTO
    {
        public string ProductName { get; set; } = null!;

        public int? CategoryId { get; set; }

        public decimal ProductUnitprice { get; set; } = 0;

        public string? ProductDetail { get; set; }

        public string? ProductImage { get; set; }
    }
}
