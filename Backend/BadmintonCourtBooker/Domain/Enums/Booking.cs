namespace Domain.Enums
{
    public enum BookingStatus
    {
        None = 0,
        Success,
        Fail,
        Pending,
        Cancel
    }

    public enum BookingOrderBy
    {
        RentDate,
        CreatedDate,
    }

    public enum BookingCheckIn
    {
        None = 0,
        True,
        False,
    }
}
