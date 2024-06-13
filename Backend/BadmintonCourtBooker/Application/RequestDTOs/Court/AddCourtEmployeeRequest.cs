using Domain.Enums;

namespace Application.RequestDTOs.Court
{
    public class AddCourtEmployeeRequest
    {
        public List<CourtEmployeeRequest> CourtEmployees { get; set; } = [];
    }

    public class CourtEmployeeRequest
    {
        public string UserId { get; set; } = string.Empty;
        public EmployeeRole Role { get; set; } = EmployeeRole.None;
    }
}
