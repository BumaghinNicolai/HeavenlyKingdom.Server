namespace HeavenlyKingdom.Domain.DTOs
{
    public class IndulgenceDto
    {
        public int Id { get; set; }
        public string Sin { get; set; } = string.Empty;
        public int Gravity { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedAt { get; set; }
    }

    public class PurchaseIndulgenceDto
    {
        public string Sin { get; set; } = string.Empty;
        public int Gravity { get; set; }
        public decimal Price { get; set; }
    }
}
