﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BookingMethodId")
                        .HasColumnType("int");

                    b.Property<bool>("CheckIn")
                        .HasColumnType("bit");

                    b.Property<Guid>("CourtId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("SlotId")
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

                    b.ToTable("Bookings");
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

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uniqueidentifier");

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

                    b.HasIndex("ManagerId");

                    b.ToTable("Courts");
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

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourtId");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
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

                    b.HasKey("Id");

                    b.HasIndex("CourtId");

                    b.ToTable("PaymentMethods");
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

                    b.HasIndex("CourtId");

                    b.ToTable("Schedules");
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

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Account")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("CreatorId")
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

                    b.ToTable("Transactions");
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

                    b.ToTable("TransactionsDetails");
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

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Booking", b =>
                {
                    b.HasOne("Domain.Entities.BookingMethod", "BookingMethod")
                        .WithMany("Bookings")
                        .HasForeignKey("BookingMethodId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Court", "Court")
                        .WithMany("Bookings")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Slot", "Slot")
                        .WithMany("Bookings")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "Manager")
                        .WithMany("ManagedCourts")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Manager");
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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("ManagedCourts");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
