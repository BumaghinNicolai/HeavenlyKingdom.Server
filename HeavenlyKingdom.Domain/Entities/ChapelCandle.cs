using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavenlyKingdom.Domain.Entities
{
    public class ChapelCandle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;
        public int SlotIndex { get; set; }
        public string Note { get; set; } = string.Empty;
        public string Intention { get; set; } = string.Empty;
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
