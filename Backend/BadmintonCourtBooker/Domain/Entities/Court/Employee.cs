using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "datetime2(0)")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2(0)")]
        public DateTime? EndDate { get; set; } = null;

        public EmployeeStatus Status { get; set; }

        public EmployeeRole Role { get; set; }

        #region NavigationProperties

        public Guid CourtId { get; set; }
        [ForeignKey(nameof(CourtId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Court Court { get; set; } = null!;

        public Guid? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public virtual User? User { get; set; }

        #endregion
    }
}
