using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.DTO.Product
{
    public class ProductsPagingDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<ProductDetailDTO>? ProductsResult { get; set; }
    }
}
