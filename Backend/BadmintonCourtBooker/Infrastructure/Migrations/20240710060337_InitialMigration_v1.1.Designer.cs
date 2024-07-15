﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240710060337_InitialMigration_v1.1")]
    partial class InitialMigration_v11
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BookingMethodId")
                        .HasColumnType("int");

                    b.Property<bool>("CheckIn")
                        .HasColumnType("bit");

                    b.Property<Guid?>("CourtId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<int?>("SlotId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionDetailId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingMethodId");

                    b.HasIndex("CourtId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SlotId");

                    b.HasIndex("TransactionDetailId")
                        .IsUnique()
                        .HasFilter("[TransactionDetailId] IS NOT NULL");

                    b.ToTable("Booking", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.BookingMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CourtId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MethodType")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerSlot")
                        .HasPrecision(11, 2)
                        .HasColumnType("decimal(11,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TimePerSlot")
                        .HasPrecision(7, 1)
                        .HasColumnType("decimal(7,1)");

                    b.HasKey("Id");

                    b.HasIndex("CourtId");

                    b.ToTable("BookingMethods");
                });

            modelBuilder.Entity("Domain.Entities.Court", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("CourtStatus")
                        .HasColumnType("int");

                    b.Property<int>("CourtType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<TimeSpan>("SlotDuration")
                        .HasColumnType("time");

                    b.Property<int>("SlotType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Court", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CourtId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("CourtId", "UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("CourtEmployee", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("CourtId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MethodType")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourtId");

                    b.ToTable("CourtPaymentMethod", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("CloseTime")
                        .HasColumnType("time");

                    b.Property<Guid>("CourtId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("OpenTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("CourtId", "DayOfWeek")
                        .IsUnique();

                    b.ToTable("CourtSchedule", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Slot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleSlot", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Account")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(11, 2)
                        .HasColumnType("decimal(11,2)");

                    b.Property<decimal>("TotalBookingTime")
                        .HasPrecision(7, 1)
                        .HasColumnType("decimal(7,1)");

                    b.Property<string>("TransactionCode")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TransactionDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(11, 2)
                        .HasColumnType("decimal(11,2)");

                    b.Property<decimal>("BookingTime")
                        .HasPrecision(7, 1)
                        .HasColumnType("decimal(7,1)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionDetail", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BookingTime")
                        .HasPrecision(7, 1)
                        .HasColumnType("decimal(7,1)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("d70e57ec-5311-4561-a665-591893953e17"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 35, 567, DateTimeKind.Utc).AddTicks(1417),
                            Email = "systemadmin@gmail.com",
                            FirstName = "Admin",
                            LastName = "System",
                            PasswordHash = "$2a$11$yIU5z6y/JRgLDBElkQ3fXunwemzc2doegoryT5jYgWTrOGc5/WVBq",
                            PhoneNumber = "0123456789",
                            Role = 4,
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("16bb9cbf-c800-497d-ba6f-472ec3732224"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 35, 689, DateTimeKind.Utc).AddTicks(149),
                            Email = "manager1@gmail.com",
                            FirstName = "Manager 1",
                            LastName = "Court",
                            PasswordHash = "$2a$11$njPEqIemeevEm8p8VToNQOxXE8HINZL9n75khLArsX9YNmmR3f3vG",
                            PhoneNumber = "0123456781",
                            Role = 3,
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("eb77ba62-6095-459f-a633-a24425109bb7"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 35, 816, DateTimeKind.Utc).AddTicks(4200),
                            Email = "manager2@gmail.com",
                            FirstName = "Manager 2",
                            LastName = "Court",
                            PasswordHash = "$2a$11$V1TB5TlRhTq88BC21V6scOY.z2XkxC/AzkYExHUyJ1kuEjgA2EaHm",
                            PhoneNumber = "0123456782",
                            Role = 3,
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("153ed6dd-c9cf-4763-abab-009771ab68ae"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 35, 943, DateTimeKind.Utc).AddTicks(7123),
                            Email = "manager3@gmail.com",
                            FirstName = "Manager 3",
                            LastName = "Court",
                            PasswordHash = "$2a$11$v.ppgwzg9YKsOdkh29IKoOoCt32L/Js/HajhyjQXv6bCvcSakIgr6",
                            PhoneNumber = "0123456783",
                            Role = 3,
                            Status = 3
                        },
                        new
                        {
                            Id = new Guid("d85c2054-bfc6-43da-9b21-8e51cc7658f1"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 36, 68, DateTimeKind.Utc).AddTicks(1312),
                            Email = "customer1@gmail.com",
                            FirstName = "Customer 1",
                            LastName = "Application",
                            PasswordHash = "$2a$11$7haoTpOUhWB96o96DzXT4.vQEyyNh/NkEmJCxkkylocnzDvZxdGHC",
                            PhoneNumber = "0123456701",
                            Role = 1,
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("246d9293-430e-4551-a501-8e27815a1f38"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 36, 197, DateTimeKind.Utc).AddTicks(9266),
                            Email = "customer2@gmail.com",
                            FirstName = "Customer 2",
                            LastName = "Application",
                            PasswordHash = "$2a$11$5WNOhj0zHZULOjz9Y1gSeeFclE5gW//qGfo4a6vuhEEShCIo9pPf6",
                            PhoneNumber = "0123456702",
                            Role = 1,
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("656edcb8-9e32-4bae-8dcd-d8dca38d2bae"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 36, 321, DateTimeKind.Utc).AddTicks(4413),
                            Email = "customer3@gmail.com",
                            FirstName = "Customer 3",
                            LastName = "Application",
                            PasswordHash = "$2a$11$7cFX9Y5.Lm5v8fAtj5mSQ.QKkfS.SAbVI3gjIEbl0z8A4AW9nMon2",
                            PhoneNumber = "0123456703",
                            Role = 1,
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("930fb3fa-f415-4667-add2-3beba3c0f382"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 36, 442, DateTimeKind.Utc).AddTicks(4731),
                            Email = "customer4@gmail.com",
                            FirstName = "Customer 4",
                            LastName = "Application",
                            PasswordHash = "$2a$11$K2uWQQ4xwwLp3MFfhFsBSe5fCnDoZj7v3vz8HMU7xr8/HlHsmK6jW",
                            PhoneNumber = "0123456704",
                            Role = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("40ace6d7-886f-4ea7-bdd0-ed67fb1774c5"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 36, 566, DateTimeKind.Utc).AddTicks(5661),
                            Email = "customer5@gmail.com",
                            FirstName = "Customer 5",
                            LastName = "Application",
                            PasswordHash = "$2a$11$OOfVOFK0AvLMgJQwPsKR4eEXItj5/csbkj3GzPiR2uX99uUevN/Bq",
                            PhoneNumber = "0123456705",
                            Role = 1,
                            Status = 3
                        },
                        new
                        {
                            Id = new Guid("069b70de-feb0-43ec-8b88-8179a628cc7e"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 36, 688, DateTimeKind.Utc).AddTicks(1406),
                            Email = "staff1@gmail.com",
                            FirstName = "Staff 1",
                            LastName = "Court",
                            PasswordHash = "$2a$11$pQayCoxpzmcyqAhkH2Vxte6BrCe7ygnpllETh9GPzyO96KlBD1vKy",
                            PhoneNumber = "0123456711",
                            Role = 2,
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("e11e90c4-f850-4b42-813f-c0eaf3326da7"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 36, 812, DateTimeKind.Utc).AddTicks(2534),
                            Email = "staff2@gmail.com",
                            FirstName = "Staff 2",
                            LastName = "Court",
                            PasswordHash = "$2a$11$hNayy8GIDNVTlad.JOvxxeIi67yvjWjDBVHNjrOfKbvmS/jJQbOr6",
                            PhoneNumber = "0123456712",
                            Role = 2,
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("8e11441e-7c37-4579-a7c2-9ca37e94ee59"),
                            BookingTime = 0m,
                            CreatedDate = new DateTime(2024, 7, 10, 6, 3, 36, 934, DateTimeKind.Utc).AddTicks(2857),
                            Email = "staff3@gmail.com",
                            FirstName = "Staff 3",
                            LastName = "Court",
                            PasswordHash = "$2a$11$jWBX81bsC9OPv6jeEVJHRubLBBoVPhix0QBK8/grorkv35bu4kI2e",
                            PhoneNumber = "0123456713",
                            Role = 2,
                            Status = 3
                        });
                });

            modelBuilder.Entity("Domain.Entities.Booking", b =>
                {
                    b.HasOne("Domain.Entities.BookingMethod", "BookingMethod")
                        .WithMany("Bookings")
                        .HasForeignKey("BookingMethodId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Court", "Court")
                        .WithMany("Bookings")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.User", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Slot", "Slot")
                        .WithMany("Bookings")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.TransactionDetail", "TransactionDetail")
                        .WithOne("Booking")
                        .HasForeignKey("Domain.Entities.Booking", "TransactionDetailId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("BookingMethod");

                    b.Navigation("Court");

                    b.Navigation("Customer");

                    b.Navigation("Slot");

                    b.Navigation("TransactionDetail");
                });

            modelBuilder.Entity("Domain.Entities.BookingMethod", b =>
                {
                    b.HasOne("Domain.Entities.Court", "Court")
                        .WithMany("BookingMethods")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Court");
                });

            modelBuilder.Entity("Domain.Entities.Court", b =>
                {
                    b.HasOne("Domain.Entities.User", "Creator")
                        .WithMany("CreatedCourts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.HasOne("Domain.Entities.Court", "Court")
                        .WithMany("Employees")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Employees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Court");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.PaymentMethod", b =>
                {
                    b.HasOne("Domain.Entities.Court", "Court")
                        .WithMany("PaymentMethods")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Court");
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.HasOne("Domain.Entities.Court", "Court")
                        .WithMany("Schedules")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Court");
                });

            modelBuilder.Entity("Domain.Entities.Slot", b =>
                {
                    b.HasOne("Domain.Entities.Schedule", "Schedule")
                        .WithMany("Slots")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Domain.Entities.User", "Creator")
                        .WithMany("Transactions")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Domain.Entities.TransactionDetail", b =>
                {
                    b.HasOne("Domain.Entities.Transaction", "Transaction")
                        .WithMany("TransactionDetails")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Domain.Entities.BookingMethod", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Domain.Entities.Court", b =>
                {
                    b.Navigation("BookingMethods");

                    b.Navigation("Bookings");

                    b.Navigation("Employees");

                    b.Navigation("PaymentMethods");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("Domain.Entities.Slot", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Navigation("TransactionDetails");
                });

            modelBuilder.Entity("Domain.Entities.TransactionDetail", b =>
                {
                    b.Navigation("Booking");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("CreatedCourts");

                    b.Navigation("Employees");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}