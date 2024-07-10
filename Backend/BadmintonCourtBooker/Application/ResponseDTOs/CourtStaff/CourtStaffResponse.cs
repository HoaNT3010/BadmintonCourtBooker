using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ResponseDTOs.CourtStaff
{
    public class StatsCourtResponse
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

    }
    //public class CheckinBookingResponse
    //{
    //    public bool CheckIn { get; set; } = false;

    //    public BookingStatus Status { get; set; } = BookingStatus.None;

    //    public DateTime RentDate { get; set; } = DateTime.UtcNow;

    //    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    //}
}
