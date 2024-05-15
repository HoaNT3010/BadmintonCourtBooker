using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        public bool CheckIn { get; set; } = false;

        public BookingStatus Status { get; set; } = BookingStatus.None;

        [Column(TypeName = "datetime2(0)")]
        public DateTime RentDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        #region NavigationProperties

        public Guid CourtId { get; set; }
        [ForeignKey(nameof(CourtId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Court Court { get; set; } = null!;

        public int SlotId { get; set; }
        [ForeignKey(nameof(SlotId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Slot Slot { get; set; } = null!;

        public int BookingMethodId { get; set; }
        [ForeignKey(nameof(BookingMethodId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual BookingMethod BookingMethod { get; set; } = null!;

        public Guid CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual User Customer { get; set; } = null!;

        public int? TransactionDetailId { get; set; }
        [ForeignKey(nameof(TransactionDetailId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual TransactionDetail? TransactionDetail { get; set; }

        #endregion
    }
}
