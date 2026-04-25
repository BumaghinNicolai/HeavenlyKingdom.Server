namespace HeavenlyKingdom.Domain.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cat { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }

    public class AddToCartDto
    {
        public string SessionId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int Qty { get; set; } = 1;
    }

    public class UpdateCartItemDto
    {
        public int Qty { get; set; }
    }
}