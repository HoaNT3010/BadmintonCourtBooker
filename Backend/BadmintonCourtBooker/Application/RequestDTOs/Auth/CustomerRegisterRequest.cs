using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.RequestDTOs.Auth
{
    public class CustomerRegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class SearchCustomerRequest
    {
       
        public string Email { get; set; } = string.Empty;
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain numbers only")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string PhoneNumber { get; set; } = string.Empty;

        public UserStatus Status { get; set; } = UserStatus.None;
    }

    public class CustomerUpdateRequest
    {
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 32 characters")]
        public string Password { get; set; } = string.Empty;
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = string.Empty;
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain numbers only")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
