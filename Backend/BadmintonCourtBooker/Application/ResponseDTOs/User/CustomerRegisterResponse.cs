using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.ResponseDTOs
{
    public class CustomerRegisterResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal BookingTime { get; set; } = 0;
        public string CreatedDate { get; set; } = string.Empty;
    }


    public class ListCustomerResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
    }


    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public class ProfileResponse
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(254)")]
        public string Email { get; set; } = null!;

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = null!;

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.None;

        public UserStatus Status { get; set; } = UserStatus.None;

        [Precision(7, 1)]
        [Range(0, 999999.9)]
        public decimal BookingTime { get; set; } = 0;

        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    
}
