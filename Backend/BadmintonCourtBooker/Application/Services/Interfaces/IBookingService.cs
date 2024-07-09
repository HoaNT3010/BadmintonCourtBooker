using Application.RequestDTOs.Booking;
using Application.ResponseDTOs.Booking;
using Infrastructure.Utilities.Paging;

namespace Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<CreateBookingResponse?> CreateSingularBooking(Guid courtId, CreateBookingRequest bookingRequest);
        Task<CreateMultipleBookingResponse?> CreateMultipleBooking(Guid courtId, CreateMultipleBookingRequest bookingRequest);
        Task<PagedList<BookingShortDetail>?> GetCurrentCustomerBookings(BookingQueryRequest queryRequest);
    }
}
