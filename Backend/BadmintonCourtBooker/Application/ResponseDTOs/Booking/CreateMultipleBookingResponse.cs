using Application.ResponseDTOs.Transaction;

namespace Application.ResponseDTOs.Booking
{
    public class CreateMultipleBookingResponse
    {
        public List<MultipleBooking> Bookings { get; set; } = [];

        public BookingCourt Court { get; set; } = null!;

        public BookingTransaction Transaction { get; set; } = null!;
    }

    public class MultipleBooking
    {
        public string Id { get; set; } = string.Empty;

        public string RentDate { get; set; } = string.Empty;

        public string StartTime { get; set; } = string.Empty;

        public string EndTime { get; set; } = string.Empty;

        public string BookingMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;
    }
}
