namespace HeavenlyKingdom.Domain.Entities
{
    public class ChapelCandle
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int SlotIndex { get; set; }
        public string Note { get; set; } = string.Empty;
        public string Intention { get; set; } = string.Empty;
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public User? User { get; set; }
    }
}
