using Application.Utilities;

namespace Application.RequestDTOs.Court
{
    public class CourtScheduleCreateRequest
    {
        public List<CourtScheduleCreate> Schedules { get; set; } = new List<CourtScheduleCreate>();

        public void CheckAndSwapOpenCloseTime()
        {
            foreach (var schedule in Schedules)
            {
                var openTime = DateTimeHelper.ConvertTimeString(schedule.OpenTime);
                var closeTime = DateTimeHelper.ConvertTimeString(schedule.CloseTime);

                // If openTime is longer (latter than) than closeTime, swap
                if (TimeSpan.Compare((TimeSpan)openTime!, (TimeSpan)closeTime!) > 0)
                {
                    (schedule.OpenTime, schedule.CloseTime) = (schedule.CloseTime, schedule.OpenTime);
                }
            }
        }
    }

    public class CourtScheduleCreate
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string OpenTime { get; set; } = string.Empty;
        public string CloseTime { get; set; } = string.Empty;
    }
}
