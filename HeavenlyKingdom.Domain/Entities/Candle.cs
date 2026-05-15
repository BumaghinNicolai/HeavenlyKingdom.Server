using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavenlyKingdom.Domain.Entities
{
    public class Candle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int Price { get; set; }
        public string BurnLabel { get; set; } = string.Empty;
        public string WaxColor { get; set; } = string.Empty;
    }
}
