namespace HeavenlyKingdom.Domain.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
        public bool IsDefault { get; set; }
    }

    public class CreateAddressDto
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
        public bool IsDefault { get; set; }
    }

    public class UpdateAddressDto
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
        public bool IsDefault { get; set; }
    }
}
