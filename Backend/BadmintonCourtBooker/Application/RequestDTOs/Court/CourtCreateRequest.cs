using Domain.Enums;

namespace Application.RequestDTOs.Court
{
    public class CourtCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public CourtType CourtType { get; set; }
        public SlotType SlotType { get; set; }
    }
}
