namespace HeavenlyKingdom.Domain.Entities
{
    public class Indulgence
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Sin { get; set; } = string.Empty;
        public int Gravity { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
    }
}
