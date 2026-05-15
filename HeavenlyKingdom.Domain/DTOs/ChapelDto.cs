namespace HeavenlyKingdom.Domain.DTOs
{
    public class ChapelCandleDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int SlotIndex { get; set; }
        public string Note { get; set; } = string.Empty;
        public string Intention { get; set; } = string.Empty;
        public DateTime PlacedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class PlaceCandleDto
    {
        public string Type { get; set; } = string.Empty;
        public int SlotIndex { get; set; }
        public string Note { get; set; } = string.Empty;
        public string Intention { get; set; } = string.Empty;
    }
}
