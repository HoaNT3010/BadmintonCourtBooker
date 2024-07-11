using Application.ResponseDTOs.Court;
using Application.ResponseDTOs.Transaction;

namespace Application.ResponseDTOs.Booking
{
    public class BookingDetail
    {
        public string Id { get; set; } = string.Empty;

        public string RentDate { get; set; } = string.Empty;

        public string StartTime { get; set; } = string.Empty;

        public string EndTime { get; set; } = string.Empty;

        public bool CheckIn { get; set; } = false;

        public string Status { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;

        public BookingCourt Court { get; set; } = null!;

        public BookingMethodDetail BookingMethod { get; set; } = null!;

        public BookingCustomer Customer { get; set; } = null!;

        public BookingTransactionDetail TransactionDetail { get; set; } = null!;
    }

    public class BookingCustomer
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class TransactionCreator : BookingCustomer
    {

    }
}
