namespace HeavenlyKingdom.Domain.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
