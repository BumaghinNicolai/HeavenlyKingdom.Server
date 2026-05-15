namespace HeavenlyKingdom.Domain.DTOs
{
    public class HolidayDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }

    public class CreateHolidayDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
