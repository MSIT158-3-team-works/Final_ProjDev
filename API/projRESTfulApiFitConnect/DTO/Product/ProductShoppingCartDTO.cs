namespace projRESTfulApiFitConnect.DTO.Product
{
    public class ProductShoppingCartDTO
    {
        public int shoppingCartId { get; set; }
        public int productId { get; set; }
        public string productName { get; set; } = null!;
        public int quantity { get; set; } = 1;
        public decimal unitPrice { get; set; }
        public string productImage { get; set; } = null!;
    }
}
