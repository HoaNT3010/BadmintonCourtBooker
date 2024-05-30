namespace Domain.Enums
{
    public enum BookingMethodType
    {
        None = 0,
        /// <summary>
        /// Fixed calendar method - Lịch cố định
        /// </summary>
        Fixed,
        /// <summary>
        /// Day method - Lịch từng ngày
        /// </summary>
        Day,
        /// <summary>
        /// Flexible calendar method - Lịch linh hoạt
        /// </summary>
        Flexible
    }
}
