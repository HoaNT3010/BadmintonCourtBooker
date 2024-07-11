using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestDTOs.SystemAdmin
{
    public class CourtUpdate
    {
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; } = null!;

        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; } = null!;

        public CourtType CourtType { get; set; } = CourtType.None;

        public SlotType SlotType { get; set; } = SlotType.None;

        [Column(TypeName = "time")]
        public TimeSpan SlotDuration { get; set; } = TimeSpan.Zero;

        public CourtStatus CourtStatus { get; set; } = CourtStatus.None;

        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
