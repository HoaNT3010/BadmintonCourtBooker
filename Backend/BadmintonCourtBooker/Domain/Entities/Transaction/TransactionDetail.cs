using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TransactionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; } = null!;

        [Precision(11, 2)]
        [Range(0, 999999999.99)]
        public decimal Amount { get; set; } = decimal.Zero;

        [Precision(7, 1)]
        [Range(0, 999999.9)]
        public decimal BookingTime { get; set; } = decimal.Zero;

        public TransactionDetailType Type { get; set; } = TransactionDetailType.None;

        #region NavigationProperties

        public Guid TransactionId { get; set; }
        [ForeignKey(nameof(TransactionId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Transaction Transaction { get; set; } = null!;

        public Booking? Booking { get; set; } = default;

        #endregion

    }
}
