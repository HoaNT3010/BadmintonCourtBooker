using Application.ResponseDTOs.Booking;

namespace Application.ResponseDTOs.Transaction
{
    public class TransactionSummary
    {
        public string Id { get; set; } = string.Empty;

        public string Account { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; } = decimal.Zero;

        public decimal TotalBookingTime { get; set; } = decimal.Zero;

        public string TransactionCode { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string CreatedDate = string.Empty;

        public TransactionCreator Creator { get; set; } = null!;

        public List<TransactionDetailSummary> TransactionDetails { get; set; } = [];
    }
}
