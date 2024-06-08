namespace Application.ResponseDTOs.Court
{
    public class CourtCreateResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CourtType { get; set; } = string.Empty;
        public string SlotType { get; set; } = string.Empty;
        public string SlotDuration { get; set; } = string.Empty;
        public string CourtStatus { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public string CreatorFullName { get; set; } = string.Empty;
        public string CreatorEmail { get; set; } = string.Empty;

    }

    public class CourtEmployee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
    }
}
