namespace HeavenlyKingdom.Domain.DTOs
{
    public class StartCandlePaymentDto
    {
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    public class ProcessCandlePaymentDto
    {
        public string CardNumber { get; set; } = string.Empty;
        public string CardHolder { get; set; } = string.Empty;
        public string Expiry    { get; set; } = string.Empty;
        public string Cvv       { get; set; } = string.Empty;
    }
}
