using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavenlyKingdom.Domain.Entities
{
    public class Father
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string San { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Parish { get; set; } = string.Empty;
        public string Diocese { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? Instagram { get; set; }
        public string? Telegram { get; set; }
        public string? Youtube { get; set; }
        public string? Facebook { get; set; }
        public string? Gmail { get; set; }
        public string? Services { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [InverseProperty("Father")]
        public List<ServiceOrder> ServiceOrders { get; set; } = new();
    }
}
