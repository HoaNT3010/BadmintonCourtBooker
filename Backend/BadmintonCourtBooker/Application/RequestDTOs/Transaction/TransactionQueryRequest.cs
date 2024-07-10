using Domain.Enums;
using Infrastructure.Utilities.Paging;

namespace Application.RequestDTOs.Transaction
{
    public class TransactionQueryRequest : BaseQueryStringParameters
    {
        public TransactionStatus Status { get; set; } = TransactionStatus.None;
        public PaymentMethodType MethodType { get; set; } = PaymentMethodType.None;
        public TransactionOrderBy OrderBy { get; set; } = TransactionOrderBy.CreateDate;
        public SortingOrder SortingOrder { get; set; } = SortingOrder.Descending;
    }
}
