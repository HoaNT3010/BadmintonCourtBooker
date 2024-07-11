namespace Application.ResponseDTOs.Transaction
{
    public class TransactionShortSummary
    {
        public string Id { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; } = decimal.Zero;

        public decimal TotalBookingTime { get; set; } = decimal.Zero;

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;
    }
}
