using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BookingMethodEntityConfiguration : IEntityTypeConfiguration<BookingMethod>
    {
        public void Configure(EntityTypeBuilder<BookingMethod> builder)
        {
            builder.ToTable("CourtBookingMethod");
        }
    }
}
