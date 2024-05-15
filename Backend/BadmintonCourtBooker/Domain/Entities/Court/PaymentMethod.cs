using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PaymentMethodType MethodType { get; set; } = PaymentMethodType.None;

        [Column(TypeName = "varchar(100)")]
        public string Account { get; set; } = null!;

        #region NavigationProperties

        public Guid CourtId { get; set; }
        [ForeignKey(nameof(CourtId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Court Court { get; set; } = null!;

        #endregion
    }
}
