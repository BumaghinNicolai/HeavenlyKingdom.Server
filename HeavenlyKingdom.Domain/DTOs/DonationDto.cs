namespace HeavenlyKingdom.Domain.DTOs
{
    public class DonationGoalDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Target { get; set; }
        public decimal Current { get; set; }
        public decimal ProgressPercent { get; set; }
    }

    public class UpdateDonationGoalDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Target { get; set; }
    }

    public class AddProgressDto
    {
        public decimal Amount { get; set; }
    }
}
