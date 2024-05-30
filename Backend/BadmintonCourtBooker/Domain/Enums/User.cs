namespace Domain.Enums
{
    public enum UserRole
    {
        None = 0,
        Customer,
        CourtStaff,
        CourtManager,
        SystemAdmin,
    }

    public enum UserStatus
    {
        None = 0,
        NotVerified,
        Active,
        Suspended,
    }
}
