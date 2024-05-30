using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        const string Password = "111111";

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.PhoneNumber).IsUnique();

            builder.HasMany(u => u.CreatedCourts)
                .WithOne(c => c.Creator)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(GetUserSeedData());
        }

        private User[] GetUserSeedData()
        {
            User[] seedData = [
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "systemadmin@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Admin",
                    LastName = "System",
                    PhoneNumber = "0123456789",
                    Role = UserRole.SystemAdmin,
                    Status = UserStatus.Active,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "manager1@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Manager 1",
                    LastName = "Court",
                    PhoneNumber = "0123456781",
                    Role = UserRole.CourtManager,
                    Status = UserStatus.Active,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "manager2@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Manager 2",
                    LastName = "Court",
                    PhoneNumber = "0123456782",
                    Role = UserRole.CourtManager,
                    Status = UserStatus.Active,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "manager3@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Manager 3",
                    LastName = "Court",
                    PhoneNumber = "0123456783",
                    Role = UserRole.CourtManager,
                    Status = UserStatus.Suspended,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "customer1@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Customer 1",
                    LastName = "Application",
                    PhoneNumber = "0123456701",
                    Role = UserRole.Customer,
                    Status = UserStatus.Active,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "customer2@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Customer 2",
                    LastName = "Application",
                    PhoneNumber = "0123456702",
                    Role = UserRole.Customer,
                    Status = UserStatus.Active,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "customer3@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Customer 3",
                    LastName = "Application",
                    PhoneNumber = "0123456703",
                    Role = UserRole.Customer,
                    Status = UserStatus.Active,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "customer4@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Customer 4",
                    LastName = "Application",
                    PhoneNumber = "0123456704",
                    Role = UserRole.Customer,
                    Status = UserStatus.NotVerified,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "customer5@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Customer 5",
                    LastName = "Application",
                    PhoneNumber = "0123456705",
                    Role = UserRole.Customer,
                    Status = UserStatus.Suspended,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "staff1@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Staff 1",
                    LastName = "Court",
                    PhoneNumber = "0123456711",
                    Role = UserRole.CourtStaff,
                    Status = UserStatus.Active,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "staff2@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Staff 2",
                    LastName = "Court",
                    PhoneNumber = "0123456712",
                    Role = UserRole.CourtStaff,
                    Status = UserStatus.Active,
                },
                new User(){
                    Id = Guid.NewGuid(),
                    Email = "staff3@gmail.com",
                    PasswordHash = BC.EnhancedHashPassword(Password),
                    FirstName = "Staff 3",
                    LastName = "Court",
                    PhoneNumber = "0123456713",
                    Role = UserRole.CourtStaff,
                    Status = UserStatus.Suspended,
                },
            ];
            return seedData;
        }
    }
}
