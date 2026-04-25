namespace HeavenlyKingdom.Domain.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Cat { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public bool IsNew { get; set; }
    }

    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Img { get; set; } = string.Empty;
        public bool IsNew { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Img { get; set; } = string.Empty;
        public bool IsNew { get; set; }
        public int CategoryId { get; set; }
    }
}