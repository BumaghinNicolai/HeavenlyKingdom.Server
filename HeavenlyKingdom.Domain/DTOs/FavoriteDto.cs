namespace HeavenlyKingdom.Domain.DTOs
{
    public class FavoriteDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImg { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductCat { get; set; } = string.Empty;
    }
}
