using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ScheduleEntityConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("CourtSchedule");

            builder.HasIndex(s => new { s.CourtId, s.DayOfWeek })
                .IsUnique();
        }
    }
}
