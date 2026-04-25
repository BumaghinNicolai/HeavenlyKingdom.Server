namespace HeavenlyKingdom.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Img { get; set; } = string.Empty;
        public bool IsNew { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}