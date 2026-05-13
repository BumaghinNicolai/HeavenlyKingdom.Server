using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavenlyKingdom.Domain.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
        public bool IsDefault { get; set; } = false;

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [InverseProperty("Address")]
        public List<Order> Orders { get; set; } = new();
    }
}
