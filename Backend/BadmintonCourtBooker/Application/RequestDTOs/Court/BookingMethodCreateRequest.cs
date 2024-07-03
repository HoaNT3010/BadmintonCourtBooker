using Domain.Enums;

namespace Application.RequestDTOs.Court
{
    public class BookingMethodCreateRequest
    {
        public List<BookingMethodCreate> BookingMethods { get; set; } = [];
    }

    public class BookingMethodCreate
    {
        public BookingMethodType Type { get; set; }
        public decimal PricePerSlot { get; set; }
        public decimal TimePerSlot { get; set; }
    }
}
