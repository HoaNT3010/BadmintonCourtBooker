namespace Application.ResponseDTOs.Booking
{
    public class BookingShortDetail
    {
        public string Id { get; set; } = string.Empty;

        public string CourtName { get; set; } = string.Empty;

        public string RentDate { get; set; } = string.Empty;

        public string StartTime { get; set; } = string.Empty;

        public string EndTime { get; set; } = string.Empty;

        public bool CheckIn { get; set; } = false;

        public string Status { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;
    }
}
