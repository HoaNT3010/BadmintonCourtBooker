using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Account { get; set; }

        [Precision(11, 2)]
        [Range(0, 999999999.99)]
        public decimal TotalAmount { get; set; } = decimal.Zero;

        [Precision(7, 1)]
        [Range(0, 999999.9)]
        public decimal TotalBookingTime { get; set; } = decimal.Zero;

        [Column(TypeName = "varchar(100)")]
        public string? TransactionCode { get; set; }

        public PaymentMethodType PaymentMethod { get; set; } = PaymentMethodType.None;

        public TransactionStatus Status { get; set; } = TransactionStatus.None;

        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        #region NavigationProperties

        public Guid? CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual User? Creator { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();

        #endregion
    }
}
