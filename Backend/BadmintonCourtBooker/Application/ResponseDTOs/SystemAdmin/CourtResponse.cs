using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ResponseDTOs.SystemAdmin
{
    public class CourtResponse
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public CourtType CourtType { get; set; } = CourtType.None;

        public SlotType SlotType { get; set; } = SlotType.None;

        public TimeSpan SlotDuration { get; set; } = TimeSpan.Zero;

        public CourtStatus CourtStatus { get; set; } = CourtStatus.None;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
