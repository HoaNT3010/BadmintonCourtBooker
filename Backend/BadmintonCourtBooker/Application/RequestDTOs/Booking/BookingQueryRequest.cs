using Domain.Enums;
using Infrastructure.Utilities.Paging;

namespace Application.RequestDTOs.Booking
{
    public class BookingQueryRequest : BaseQueryStringParameters
    {
        public BookingCheckIn CheckIn { get; set; } = BookingCheckIn.None;
        public BookingStatus Status { get; set; } = BookingStatus.None;
        public BookingOrderBy OrderBy { get; set; } = BookingOrderBy.RentDate;
        public SortingOrder SortingOrder { get; set; } = SortingOrder.Descending;
    }
}
