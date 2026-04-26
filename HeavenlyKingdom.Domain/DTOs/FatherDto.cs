namespace HeavenlyKingdom.Domain.DTOs
{
    public class FatherDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string San { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public string? Instagram { get; set; }
        public string? Telegram { get; set; }
        public string? Youtube { get; set; }
        public string? Facebook { get; set; }
        public string? Gmail { get; set; }
    }

    public class CreateFatherDto
    {
        public string Name { get; set; } = string.Empty;
        public string San { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public string? Instagram { get; set; }
        public string? Telegram { get; set; }
        public string? Youtube { get; set; }
        public string? Facebook { get; set; }
        public string? Gmail { get; set; }
    }

    public class UpdateFatherDto : CreateFatherDto { }
}