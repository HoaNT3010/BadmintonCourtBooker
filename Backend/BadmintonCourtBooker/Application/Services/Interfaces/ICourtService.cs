using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;

namespace Application.Services.Interfaces
{
    public interface ICourtService
    {
        Task<CourtCreateResponse?> CreateNewCourt(CourtCreateRequest createRequest);
        Task<CourtDetail?> AddCourtSchedule(Guid id, CourtScheduleCreateRequest createRequest);
        Task<CourtDetail?> GetCourtDetail(Guid id);
        Task<CourtDetail?> AddCourtEmployees(Guid id, AddCourtEmployeeRequest request);
        Task<CourtDetail?> AddCourtPaymentMethods(Guid id, PaymentMethodCreateRequest request);
        Task<CourtDetail?> AddCourtBookingMethods(Guid id, BookingMethodCreateRequest request);
    }
}
