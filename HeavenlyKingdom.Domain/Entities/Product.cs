using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavenlyKingdom.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Img { get; set; } = string.Empty;
        public bool IsNew { get; set; }
        public bool OnSale { get; set; } = false;
        public decimal? SalePrice { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        [InverseProperty("Product")]
        public List<CartItem> CartItems { get; set; } = new();

        [InverseProperty("Product")]
        public List<OrderItem> OrderItems { get; set; } = new();

        [InverseProperty("Product")]
        public List<Favorite> Favorites { get; set; } = new();
    }
}
