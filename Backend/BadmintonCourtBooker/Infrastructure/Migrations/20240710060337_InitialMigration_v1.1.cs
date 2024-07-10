using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_v11 : Migration
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
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id");
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
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CourtPaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodType = table.Column<int>(type: "int", nullable: false),
                    Account = table.Column<string>(type: "varchar(100)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    CourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SlotId = table.Column<int>(type: "int", nullable: true),
                    BookingMethodId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    { new Guid("069b70de-feb0-43ec-8b88-8179a628cc7e"), 0m, new DateTime(2024, 7, 10, 6, 3, 36, 688, DateTimeKind.Utc).AddTicks(1406), "staff1@gmail.com", "Staff 1", "Court", "$2a$11$pQayCoxpzmcyqAhkH2Vxte6BrCe7ygnpllETh9GPzyO96KlBD1vKy", "0123456711", 2, 2 },
                    { new Guid("153ed6dd-c9cf-4763-abab-009771ab68ae"), 0m, new DateTime(2024, 7, 10, 6, 3, 35, 943, DateTimeKind.Utc).AddTicks(7123), "manager3@gmail.com", "Manager 3", "Court", "$2a$11$v.ppgwzg9YKsOdkh29IKoOoCt32L/Js/HajhyjQXv6bCvcSakIgr6", "0123456783", 3, 3 },
                    { new Guid("16bb9cbf-c800-497d-ba6f-472ec3732224"), 0m, new DateTime(2024, 7, 10, 6, 3, 35, 689, DateTimeKind.Utc).AddTicks(149), "manager1@gmail.com", "Manager 1", "Court", "$2a$11$njPEqIemeevEm8p8VToNQOxXE8HINZL9n75khLArsX9YNmmR3f3vG", "0123456781", 3, 2 },
                    { new Guid("246d9293-430e-4551-a501-8e27815a1f38"), 0m, new DateTime(2024, 7, 10, 6, 3, 36, 197, DateTimeKind.Utc).AddTicks(9266), "customer2@gmail.com", "Customer 2", "Application", "$2a$11$5WNOhj0zHZULOjz9Y1gSeeFclE5gW//qGfo4a6vuhEEShCIo9pPf6", "0123456702", 1, 2 },
                    { new Guid("40ace6d7-886f-4ea7-bdd0-ed67fb1774c5"), 0m, new DateTime(2024, 7, 10, 6, 3, 36, 566, DateTimeKind.Utc).AddTicks(5661), "customer5@gmail.com", "Customer 5", "Application", "$2a$11$OOfVOFK0AvLMgJQwPsKR4eEXItj5/csbkj3GzPiR2uX99uUevN/Bq", "0123456705", 1, 3 },
                    { new Guid("656edcb8-9e32-4bae-8dcd-d8dca38d2bae"), 0m, new DateTime(2024, 7, 10, 6, 3, 36, 321, DateTimeKind.Utc).AddTicks(4413), "customer3@gmail.com", "Customer 3", "Application", "$2a$11$7cFX9Y5.Lm5v8fAtj5mSQ.QKkfS.SAbVI3gjIEbl0z8A4AW9nMon2", "0123456703", 1, 2 },
                    { new Guid("8e11441e-7c37-4579-a7c2-9ca37e94ee59"), 0m, new DateTime(2024, 7, 10, 6, 3, 36, 934, DateTimeKind.Utc).AddTicks(2857), "staff3@gmail.com", "Staff 3", "Court", "$2a$11$jWBX81bsC9OPv6jeEVJHRubLBBoVPhix0QBK8/grorkv35bu4kI2e", "0123456713", 2, 3 },
                    { new Guid("930fb3fa-f415-4667-add2-3beba3c0f382"), 0m, new DateTime(2024, 7, 10, 6, 3, 36, 442, DateTimeKind.Utc).AddTicks(4731), "customer4@gmail.com", "Customer 4", "Application", "$2a$11$K2uWQQ4xwwLp3MFfhFsBSe5fCnDoZj7v3vz8HMU7xr8/HlHsmK6jW", "0123456704", 1, 1 },
                    { new Guid("d70e57ec-5311-4561-a665-591893953e17"), 0m, new DateTime(2024, 7, 10, 6, 3, 35, 567, DateTimeKind.Utc).AddTicks(1417), "systemadmin@gmail.com", "Admin", "System", "$2a$11$yIU5z6y/JRgLDBElkQ3fXunwemzc2doegoryT5jYgWTrOGc5/WVBq", "0123456789", 4, 2 },
                    { new Guid("d85c2054-bfc6-43da-9b21-8e51cc7658f1"), 0m, new DateTime(2024, 7, 10, 6, 3, 36, 68, DateTimeKind.Utc).AddTicks(1312), "customer1@gmail.com", "Customer 1", "Application", "$2a$11$7haoTpOUhWB96o96DzXT4.vQEyyNh/NkEmJCxkkylocnzDvZxdGHC", "0123456701", 1, 2 },
                    { new Guid("e11e90c4-f850-4b42-813f-c0eaf3326da7"), 0m, new DateTime(2024, 7, 10, 6, 3, 36, 812, DateTimeKind.Utc).AddTicks(2534), "staff2@gmail.com", "Staff 2", "Court", "$2a$11$hNayy8GIDNVTlad.JOvxxeIi67yvjWjDBVHNjrOfKbvmS/jJQbOr6", "0123456712", 2, 2 },
                    { new Guid("eb77ba62-6095-459f-a633-a24425109bb7"), 0m, new DateTime(2024, 7, 10, 6, 3, 35, 816, DateTimeKind.Utc).AddTicks(4200), "manager2@gmail.com", "Manager 2", "Court", "$2a$11$V1TB5TlRhTq88BC21V6scOY.z2XkxC/AzkYExHUyJ1kuEjgA2EaHm", "0123456782", 3, 2 }
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
                name: "IX_CourtEmployee_CourtId_UserId",
                table: "CourtEmployee",
                columns: new[] { "CourtId", "UserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CourtEmployee_UserId",
                table: "CourtEmployee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtPaymentMethod_CourtId",
                table: "CourtPaymentMethod",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtSchedule_CourtId_DayOfWeek",
                table: "CourtSchedule",
                columns: new[] { "CourtId", "DayOfWeek" },
                unique: true);

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
