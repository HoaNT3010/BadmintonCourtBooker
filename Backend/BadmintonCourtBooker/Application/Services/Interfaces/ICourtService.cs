using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;

namespace Application.Services.Interfaces
{
    public interface ICourtService
    {
        Task<CourtCreateResponse?> CreateNewCourt(CourtCreateRequest createRequest);
        Task<CourtDetail?> AddCourtSchedule(Guid id, CourtScheduleCreateRequest createRequest);
    }
}
