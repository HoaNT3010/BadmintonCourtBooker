using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(254)")]
        public string Email { get; set; } = null!;

        [Column(TypeName = "varchar(200)")]
        public string PasswordHash { get; set; } = null!;

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

        #region NavigationProperties
        [JsonIgnore]
        public virtual ICollection<Court> CreatedCourts { get; set; } = new List<Court>();
        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        #endregion
    }
}
