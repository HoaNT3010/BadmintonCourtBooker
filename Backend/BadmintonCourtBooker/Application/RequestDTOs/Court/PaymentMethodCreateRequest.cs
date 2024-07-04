using Domain.Enums;

namespace Application.RequestDTOs.Court
{
    public class PaymentMethodCreateRequest
    {
        public List<PaymentMethodCreate> PaymentMethods { get; set; } = [];
    }

    public class PaymentMethodCreate
    {
        public PaymentMethodType Type { get; set; }
        public string Account { get; set; } = string.Empty;
    }
}
