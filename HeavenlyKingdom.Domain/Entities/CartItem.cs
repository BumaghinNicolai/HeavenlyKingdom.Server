namespace HeavenlyKingdom.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = string.Empty;  // идентификатор корзины пользователя
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}