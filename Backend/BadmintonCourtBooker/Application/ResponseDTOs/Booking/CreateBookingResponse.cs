using Application.ResponseDTOs.Transaction;

namespace Application.ResponseDTOs.Booking
{
    public class CreateBookingResponse
    {
        public string Id { get; set; } = string.Empty;

        public string RentDate { get; set; } = string.Empty;

        public string StartTime { get; set; } = string.Empty;

        public string EndTime { get; set; } = string.Empty;

        public string BookingMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;

        public BookingCourt Court { get; set; } = null!;

        public BookingTransaction Transaction { get; set; } = null!;
    }

    public class BookingCourt
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
