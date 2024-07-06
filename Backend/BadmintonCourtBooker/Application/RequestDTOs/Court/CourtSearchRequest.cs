using Domain.Enums;
using Infrastructure.Utilities.Paging;

namespace Application.RequestDTOs.Court
{
    public class CourtSearchRequest : BaseQueryStringParameters
    {
        public string CourtName { get; set; } = string.Empty;
        public string CourtLocation { get; set; } = string.Empty;
        public CourtType CourtType { get; set; } = CourtType.None;
        public CourtStatus CourtStatus { get; set; } = CourtStatus.None;
        public CourtOrderBy OrderBy { get; set; } = CourtOrderBy.CourtName;
        public SortingOrder SortingOrder { get; set; } = SortingOrder.Ascending;
    }
}
