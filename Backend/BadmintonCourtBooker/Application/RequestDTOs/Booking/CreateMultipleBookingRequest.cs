using Application.Utilities;

namespace Application.RequestDTOs.Booking
{
    public class CreateMultipleBookingRequest
    {
        public int SlotId { get; set; }
        public int BookingMethodId { get; set; }
        public int PaymentMethodId { get; set; }
        public List<BookingRentDate> RentDates { get; set; } = [];
    }

    public class BookingRentDate
    {
        /// <summary>
        /// Rent date of the booking request. Must follow format dd/MM/yyyy (ex: 08/07/2024)
        /// </summary>
        public string RentDate { get; set; } = string.Empty;

        public DateTime? GetRentDate()
        {
            return DateTimeHelper.ConvertDateString(RentDate);
        }
    }
}
