namespace Domain.Enums
{
    public enum PaymentMethodType
    {
        None = 0,
        OnCourt,
        MoMo,
        // Can add other payment method (Ex: VnPay, Online banking services,...)
    }

    public enum PaymentMethodStatus
    {
        None = 0,
        Active,
        Deactivated,
    }
}
