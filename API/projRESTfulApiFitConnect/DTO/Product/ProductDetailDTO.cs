using projRESTfulApiFitConnect.DTO.Coach;
using System.Data.SqlTypes;

namespace projRESTfulApiFitConnect.DTO.Product
{
    public class ProductDetailDTO
    {
        public int productId { get; set; }
        public string productName { get; set; } = null!;
        public int productCategoryId { get; set; } = 0;
        public string productCategory { get; set; } = null!;
        public decimal unitPrice { get; set; }
        public string? productDetail { get; set; }
        public string productImage { get; set; } = null!;
        public int productSold { get; set; } = 0;
        public List<ProductImagesDTO>? Images { get; set; }
        public List<string>? Base64Images { get; set; }
    }
}
