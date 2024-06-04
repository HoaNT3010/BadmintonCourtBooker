namespace Application.ResponseDTOs
{
    public class CustomerRegisterResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal BookingTime { get; set; } = 0;
        public string CreatedDate { get; set; } = string.Empty;
    }
}
