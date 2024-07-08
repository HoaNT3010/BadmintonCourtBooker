namespace Application.ResponseDTOs.Transaction
{
    public class BookingTransaction
    {
        public string Id { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; } = decimal.Zero;

        public decimal TotalBookingTime { get; set; } = decimal.Zero;

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string CreatedDate = string.Empty;

        public List<BookingTransactionDetail> TransactionDetails { get; set; } = [];
    }
}
