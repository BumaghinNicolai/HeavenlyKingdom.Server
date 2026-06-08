using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavenlyKingdom.Domain.Entities
{
    public class Indulgence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Sin { get; set; } = string.Empty;
        public int Gravity { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Indulgences")]
        public User? User { get; set; }
    }
}
