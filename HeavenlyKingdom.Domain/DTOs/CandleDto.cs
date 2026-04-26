namespace HeavenlyKingdom.Domain.DTOs
{
    public class CandleDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int Price { get; set; }
        public string BurnLabel { get; set; } = string.Empty;
        public string WaxColor { get; set; } = string.Empty;
    }

    public class CreateCandleDto
    {
        public string Type { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int Price { get; set; }
        public string BurnLabel { get; set; } = string.Empty;
        public string WaxColor { get; set; } = string.Empty;
    }

    public class UpdateCandleDto : CreateCandleDto { }
}