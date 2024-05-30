using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Slot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan StartTime { get; set; } = TimeSpan.Zero;

        [Column(TypeName = "time")]
        public TimeSpan EndTime { get; set; } = TimeSpan.Zero;

        public bool Available { get; set; } = true;

        #region NavigationProperties

        public int ScheduleId { get; set; }
        [ForeignKey(nameof(ScheduleId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Schedule Schedule { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        #endregion
    }
}
