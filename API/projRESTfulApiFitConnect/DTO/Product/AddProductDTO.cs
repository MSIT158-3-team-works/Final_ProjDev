namespace projRESTfulApiFitConnect.DTO.Product
{
    public class AddProductDTO
    {
        public string ProductName { get; set; } = null!;

        public int? CategoryId { get; set; }

        public decimal ProductUnitprice { get; set; } = 0;

        public string? ProductDetail { get; set; }

        public string? ProductImage { get; set; }

        public string? ImageBase64 { get; set; }
        public List<ProductImagesDTO>? Images { get; set; }
        public List<string>? moreBase64Images { get; set; }
    }
}
