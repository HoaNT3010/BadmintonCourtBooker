using Application.ResponseDTOs.CourtStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICourtStaffService
    {
        Task<(List<StatsCourtResponse>, List<BookingViewBySlot>)> ViewStatsOfCourt(Guid id);
        Task<bool> CourtCheckin(Guid id, string phone);

    }
}
