using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(200)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BookingTime = table.Column<decimal>(type: "decimal(7,1)", precision: 7, scale: 1, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Court",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CourtType = table.Column<int>(type: "int", nullable: false),
                    SlotType = table.Column<int>(type: "int", nullable: false),
                    SlotDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CourtStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Court", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Court_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Account = table.Column<string>(type: "varchar(100)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    TotalBookingTime = table.Column<decimal>(type: "decimal(7,1)", precision: 7, scale: 1, nullable: false),
                    TransactionCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodType = table.Column<int>(type: "int", nullable: false),
                    PricePerSlot = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    TimePerSlot = table.Column<decimal>(type: "decimal(7,1)", precision: 7, scale: 1, nullable: false),
                    CourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingMethods_Court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourtEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtEmployee_Court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourtEmployee_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourtPaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodType = table.Column<int>(type: "int", nullable: false),
                    Account = table.Column<string>(type: "varchar(100)", nullable: false),
                    CourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtPaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtPaymentMethod_Court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourtSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    OpenTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtSchedule_Court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    BookingTime = table.Column<decimal>(type: "decimal(7,1)", precision: 7, scale: 1, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDetail_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleSlot_CourtSchedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "CourtSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckIn = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SlotId = table.Column<int>(type: "int", nullable: false),
                    BookingMethodId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_BookingMethods_BookingMethodId",
                        column: x => x.BookingMethodId,
                        principalTable: "BookingMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Booking_Court_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Court",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Booking_ScheduleSlot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "ScheduleSlot",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Booking_TransactionDetail_TransactionDetailId",
                        column: x => x.TransactionDetailId,
                        principalTable: "TransactionDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Booking_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BookingTime", "CreatedDate", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role", "Status" },
                values: new object[,]
                {
                    { new Guid("10852e2f-a4e8-4c55-acc7-dc73eecb79af"), 0m, new DateTime(2024, 5, 30, 10, 20, 31, 168, DateTimeKind.Utc).AddTicks(2666), "staff1@gmail.com", "Staff 1", "Court", "$2a$11$U/epyaOjAa9B6xhP7mNqfu78AUDUFmYa.cYNzy3JXIF9pgObAYcI2", "0123456711", 2, 2 },
                    { new Guid("1d797b74-2b07-47ea-b467-afd71a25ec7e"), 0m, new DateTime(2024, 5, 30, 10, 20, 31, 294, DateTimeKind.Utc).AddTicks(2683), "staff2@gmail.com", "Staff 2", "Court", "$2a$11$LhbNNHokIxIGfFI1H9V3o.AZDtSSHJNah6W1bvYNaSwpKfPp.87Ni", "0123456712", 2, 2 },
                    { new Guid("283626f5-1884-4dcf-8c21-02467533a323"), 0m, new DateTime(2024, 5, 30, 10, 20, 30, 919, DateTimeKind.Utc).AddTicks(245), "customer4@gmail.com", "Customer 4", "Application", "$2a$11$9yRycyAjrp3vWPRW4NLsk.ldKI94irUq6BnzutYDgYpTZz.xHcYbq", "0123456704", 1, 1 },
                    { new Guid("40d95dd2-0ff8-4ad7-8533-d79f22b2a340"), 0m, new DateTime(2024, 5, 30, 10, 20, 30, 24, DateTimeKind.Utc).AddTicks(8161), "systemadmin@gmail.com", "Admin", "System", "$2a$11$mbOfFKZU4799J/4dE92PpOA83OQfS13xcoc159rpjdr.62OXtKnla", "0123456789", 4, 2 },
                    { new Guid("5a9104cf-ff64-4f2b-a90c-6c6a7cbf15cf"), 0m, new DateTime(2024, 5, 30, 10, 20, 30, 282, DateTimeKind.Utc).AddTicks(7020), "manager2@gmail.com", "Manager 2", "Court", "$2a$11$SXQk52JqIeTUzgvwRX678.5qxrnsK7jH29qeFCH10iOUc4E9mTH/6", "0123456782", 3, 2 },
                    { new Guid("6af47088-5036-4bfc-a0dc-f7ebd3f3f701"), 0m, new DateTime(2024, 5, 30, 10, 20, 30, 153, DateTimeKind.Utc).AddTicks(573), "manager1@gmail.com", "Manager 1", "Court", "$2a$11$Bz.FJz1zwrLDrkG8XjXuf.dzerTiess2GTorh/bp/rliThcOYkAbu", "0123456781", 3, 2 },
                    { new Guid("7166467d-cb8d-4fe6-8dcb-e7243903cbff"), 0m, new DateTime(2024, 5, 30, 10, 20, 30, 668, DateTimeKind.Utc).AddTicks(6408), "customer2@gmail.com", "Customer 2", "Application", "$2a$11$breyP7ioNRfF7jeVK6k.surMWPzW5M3JcL/iazJUCuZYFYKB8JSdO", "0123456702", 1, 2 },
                    { new Guid("9cb54195-05c7-404a-9263-7888d3868db1"), 0m, new DateTime(2024, 5, 30, 10, 20, 31, 418, DateTimeKind.Utc).AddTicks(6196), "staff3@gmail.com", "Staff 3", "Court", "$2a$11$pRdKFcp91c6NSOmXLCbLneuvD9hVCv9iUxArW7u4Q1O6AWUYRCtCa", "0123456713", 2, 3 },
                    { new Guid("a09fb0ec-1167-4ba8-87e7-cfc848f1bbd8"), 0m, new DateTime(2024, 5, 30, 10, 20, 30, 794, DateTimeKind.Utc).AddTicks(1564), "customer3@gmail.com", "Customer 3", "Application", "$2a$11$u1HDZzAaHFxN/0lfwXXIWe0fWypqECr1Fsht83i9Ev9IA4WVX41gy", "0123456703", 1, 2 },
                    { new Guid("a71ee58a-3738-400c-9268-79e2987970a1"), 0m, new DateTime(2024, 5, 30, 10, 20, 30, 415, DateTimeKind.Utc).AddTicks(5909), "manager3@gmail.com", "Manager 3", "Court", "$2a$11$7w8E.pC6RwhNEA.oPgttlO6X79ZplK5lzF8IaAcZ71/BEzG46qLDq", "0123456783", 3, 3 },
                    { new Guid("f7b17042-048d-4777-9380-4057b701bc92"), 0m, new DateTime(2024, 5, 30, 10, 20, 31, 44, DateTimeKind.Utc).AddTicks(1444), "customer5@gmail.com", "Customer 5", "Application", "$2a$11$9NzDptsngIFbq.bMwq63dextEPAvt2ttxPcj4KgNHRKADtxpsz2g.", "0123456705", 1, 3 },
                    { new Guid("fd1f4ed3-895d-498e-a882-4b181e476f59"), 0m, new DateTime(2024, 5, 30, 10, 20, 30, 541, DateTimeKind.Utc).AddTicks(1395), "customer1@gmail.com", "Customer 1", "Application", "$2a$11$nWEQ4dnkPG1y6zKQguRFIetPii4FjdGQCZpPC8cYB5H0VkRgnfPa6", "0123456701", 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingMethodId",
                table: "Booking",
                column: "BookingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CourtId",
                table: "Booking",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CustomerId",
                table: "Booking",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SlotId",
                table: "Booking",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TransactionDetailId",
                table: "Booking",
                column: "TransactionDetailId",
                unique: true,
                filter: "[TransactionDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookingMethods_CourtId",
                table: "BookingMethods",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Court_CreatorId",
                table: "Court",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtEmployee_CourtId",
                table: "CourtEmployee",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtEmployee_UserId",
                table: "CourtEmployee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtPaymentMethod_CourtId",
                table: "CourtPaymentMethod",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtSchedule_CourtId",
                table: "CourtSchedule",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSlot_ScheduleId",
                table: "ScheduleSlot",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CreatorId",
                table: "Transaction",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetail_TransactionId",
                table: "TransactionDetail",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                table: "User",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "CourtEmployee");

            migrationBuilder.DropTable(
                name: "CourtPaymentMethod");

            migrationBuilder.DropTable(
                name: "BookingMethods");

            migrationBuilder.DropTable(
                name: "ScheduleSlot");

            migrationBuilder.DropTable(
                name: "TransactionDetail");

            migrationBuilder.DropTable(
                name: "CourtSchedule");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Court");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
