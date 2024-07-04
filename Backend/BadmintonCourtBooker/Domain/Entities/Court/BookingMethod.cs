using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BookingMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public BookingMethodType MethodType { get; set; } = BookingMethodType.None;

        [Precision(11, 2)]
        [Range(0, 999999999.99)]
        public decimal PricePerSlot { get; set; } = decimal.Zero;

        [Precision(7, 1)]
        [Range(0, 999999.9)]
        public decimal TimePerSlot { get; set; } = decimal.Zero;

        public BookingMethodStatus Status { get; set; } = BookingMethodStatus.None;

        #region NavigationProperties

        public Guid CourtId { get; set; }
        [ForeignKey(nameof(CourtId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Court Court { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        #endregion
    }
}
