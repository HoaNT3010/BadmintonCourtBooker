using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;
using Infrastructure.Utilities.Paging;

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
        Task<bool> ActivateCourt(Guid id);
        Task<bool> DeactivateCourt(Guid id);
        Task<PagedList<CourtShortDetail>> SearchCourt(CourtSearchRequest searchRequest);
        Task<bool> SoftDeleteCourt(Guid id);
        Task<CourtDetail> UpdateCourtById(Guid courtId, CourtUpdateRequest courtInfoRequest);
        Task<CourtDetail?> UpdateCourtPaymentMethods(Guid id, int requestPaymentMethodId);
    }
}
