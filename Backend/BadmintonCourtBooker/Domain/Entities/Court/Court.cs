﻿using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Court
    {
        [Key]
        public Guid Id { get; set; }

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

        #region NavigationProperties

        public Guid? CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public virtual User? Creator { get; set; }

        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public virtual ICollection<BookingMethod> BookingMethods { get; set; } = new List<BookingMethod>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        #endregion
    }
}
