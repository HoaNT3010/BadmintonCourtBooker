using Application.ResponseDTOs.Booking;
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
        Task<BookingShortDetail> CourtCheckin(Guid id, Guid bookingid);

    }
}
