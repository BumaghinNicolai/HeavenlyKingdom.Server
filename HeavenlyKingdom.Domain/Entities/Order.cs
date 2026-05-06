namespace HeavenlyKingdom.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;

        public List<OrderItem> Items { get; set; } = new();
    }
}
