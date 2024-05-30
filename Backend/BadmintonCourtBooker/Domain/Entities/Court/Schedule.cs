using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; } = DayOfWeek.Sunday;

        [Column(TypeName = "time")]
        public TimeSpan OpenTime { get; set; } = TimeSpan.Zero;

        [Column(TypeName = "time")]
        public TimeSpan CloseTime { get; set; } = TimeSpan.Zero;

        #region NavigationProperties

        public Guid CourtId { get; set; }
        [ForeignKey(nameof(CourtId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Court Court { get; set; } = null!;

        public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

        #endregion
    }
}
