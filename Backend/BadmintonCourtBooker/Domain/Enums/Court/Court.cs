namespace Domain.Enums
{
    public enum CourtType
    {
        None = 0,
        Indoor,
        Outdoor,
    }

    public enum SlotType
    {
        None = 0,
        OneHour,
        OneHourHalf,
        TwoHour,
        TwoHourHalf,
        ThreeHour,
    }

    public enum CourtStatus
    {
        None = 0,
        Active,
        Inactive,
        Removed,
    }
}
