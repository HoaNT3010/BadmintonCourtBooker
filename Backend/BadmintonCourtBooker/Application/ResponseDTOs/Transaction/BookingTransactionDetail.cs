using Domain.Enums;

namespace Application.ResponseDTOs.Transaction
{
    public class BookingTransactionDetail
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public decimal Amount { get; set; } = decimal.Zero;

        public decimal BookingTime { get; set; } = decimal.Zero;

        public string Type { get; set; } = string.Empty;
    }
}
