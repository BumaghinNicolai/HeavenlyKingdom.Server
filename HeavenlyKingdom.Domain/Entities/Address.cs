namespace HeavenlyKingdom.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
        public bool IsDefault { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
