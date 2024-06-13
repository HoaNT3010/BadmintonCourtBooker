namespace Application.ResponseDTOs.Court
{
    public class CourtDetail
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CourtType { get; set; } = string.Empty;
        public string SlotType { get; set; } = string.Empty;
        public string SlotDuration { get; set; } = string.Empty;
        public string CourtStatus { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public CourtCreatorDetail? Creator { get; set; }
        public List<CourtEmployee> Employees { get; set; } = [];
        public List<ScheduleDetail> Schedules { get; set; } = [];
        public List<PaymentMethodDetail> PaymentMethods { get; set; } = [];
        public List<BookingMethodDetail> BookingMethods { get; set; } = [];
    }

    public class CourtCreatorDetail
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class ScheduleDetail
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public string OpenTime { get; set; } = string.Empty;
        public string CloseTime { get; set; } = string.Empty;
        public List<SlotDetail> Slots { get; set; } = [];
    }

    public class SlotDetail
    {
        public int Id { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public bool Available { get; set; } = false;
    }

    public class PaymentMethodDetail
    {
        public int Id { get; set; }

        public string MethodType { get; set; } = string.Empty;

        public string Account { get; set; } = string.Empty;
    }

    public class BookingMethodDetail
    {
        public int Id { get; set; }

        public string MethodType { get; set; } = string.Empty;

        public decimal PricePerSlot { get; set; } = decimal.Zero;

        public decimal TimePerSlot { get; set; } = decimal.Zero;
    }
}
