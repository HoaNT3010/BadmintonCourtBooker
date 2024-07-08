using Application.RequestDTOs.Booking;
using Application.ResponseDTOs.Booking;

namespace Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<CreateBookingResponse?> CreateSingularBooking(Guid courtId, CreateBookingRequest bookingRequest);
    }
}
