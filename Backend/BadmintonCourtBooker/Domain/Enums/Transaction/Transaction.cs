namespace Domain.Enums
{
    public enum TransactionStatus
    {
        None = 0,
        Success,
        Fail,
        Pending,
        Cancel
    }

    public enum TransactionOrderBy
    {
        CreateDate,
        TotalAmount,
        TotalBookingTime,
    }
}
