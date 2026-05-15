using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavenlyKingdom.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public bool IsFather { get; set; } = false;

        [InverseProperty("User")]
        public List<Address> Addresses { get; set; } = new();

        [InverseProperty("User")]
        public List<Notification> Notifications { get; set; } = new();

        [InverseProperty("User")]
        public List<Order> Orders { get; set; } = new();

        [InverseProperty("User")]
        public List<Favorite> Favorites { get; set; } = new();

        [InverseProperty("User")]
        public List<ChapelCandle> ChapelCandles { get; set; } = new();

        [InverseProperty("User")]
        public List<Indulgence> Indulgences { get; set; } = new();
    }
}
