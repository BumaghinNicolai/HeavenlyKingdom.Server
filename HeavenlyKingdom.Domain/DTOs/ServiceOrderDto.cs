namespace HeavenlyKingdom.Domain.DTOs
{
    public class ServiceOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? FatherId { get; set; }
        public string? FatherName { get; set; }
        public string ServiceId { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class CreateServiceOrderDto
    {
        public int? FatherId { get; set; }
        public string ServiceId { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
