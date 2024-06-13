using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_CreatorId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_CourtSchedule_CourtId",
                table: "CourtSchedule");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("10852e2f-a4e8-4c55-acc7-dc73eecb79af"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1d797b74-2b07-47ea-b467-afd71a25ec7e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("283626f5-1884-4dcf-8c21-02467533a323"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("40d95dd2-0ff8-4ad7-8533-d79f22b2a340"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5a9104cf-ff64-4f2b-a90c-6c6a7cbf15cf"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("6af47088-5036-4bfc-a0dc-f7ebd3f3f701"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("7166467d-cb8d-4fe6-8dcb-e7243903cbff"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("9cb54195-05c7-404a-9263-7888d3868db1"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a09fb0ec-1167-4ba8-87e7-cfc848f1bbd8"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a71ee58a-3738-400c-9268-79e2987970a1"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("f7b17042-048d-4777-9380-4057b701bc92"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fd1f4ed3-895d-498e-a882-4b181e476f59"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CourtEmployee",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "SlotId",
                table: "Booking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "BookingMethodId",
                table: "Booking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BookingTime", "CreatedDate", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role", "Status" },
                values: new object[,]
                {
                    { new Guid("012af60d-02b0-4053-be69-bf6264691597"), 0m, new DateTime(2024, 6, 12, 3, 44, 2, 823, DateTimeKind.Utc).AddTicks(3354), "staff2@gmail.com", "Staff 2", "Court", "$2a$11$Atd8QY.p.bE6zf5NGgoZQuFmy.MTLS/qXQrkxEwqhYByyNA0y3Q9m", "0123456712", 2, 2 },
                    { new Guid("0b178799-a492-41ac-9d9a-c2a2dafe7dc6"), 0m, new DateTime(2024, 6, 12, 3, 44, 1, 699, DateTimeKind.Utc).AddTicks(6399), "manager1@gmail.com", "Manager 1", "Court", "$2a$11$.5ch/rIirwEIiI3WPEwoBOk0S.3l2c.j/RQF7hszDBjXzqe0zqvEC", "0123456781", 3, 2 },
                    { new Guid("0ddd584f-ce6d-48d8-b55c-edbdb75d9a88"), 0m, new DateTime(2024, 6, 12, 3, 44, 2, 205, DateTimeKind.Utc).AddTicks(3264), "customer2@gmail.com", "Customer 2", "Application", "$2a$11$ZEohmPZJJcnu9fLl9QUC0eWmDjtLcP2trzJwqPRP5J911KymN8F1e", "0123456702", 1, 2 },
                    { new Guid("159aced1-f67a-4c34-be93-3e0009dd6419"), 0m, new DateTime(2024, 6, 12, 3, 44, 2, 330, DateTimeKind.Utc).AddTicks(9180), "customer3@gmail.com", "Customer 3", "Application", "$2a$11$T8851XFlpsAnq7BlxtrS5OG4jT393Y4LLW/7CjQVzd.31lpcVX6ny", "0123456703", 1, 2 },
                    { new Guid("1a030c5f-4067-4c83-a985-98608f0d7c4f"), 0m, new DateTime(2024, 6, 12, 3, 44, 1, 825, DateTimeKind.Utc).AddTicks(9093), "manager2@gmail.com", "Manager 2", "Court", "$2a$11$k9d2hq.A284.1OVYUhyYxuOqLs0KzkncmClqtbrYdggnKw3LuNEtm", "0123456782", 3, 2 },
                    { new Guid("2936edbc-9bad-4549-a3e4-2bfa444feb54"), 0m, new DateTime(2024, 6, 12, 3, 44, 2, 700, DateTimeKind.Utc).AddTicks(574), "staff1@gmail.com", "Staff 1", "Court", "$2a$11$UZuuuRW1tMTyLbplV44ZGOO6euUhUyS0IIp/xn7UCPwHAq7mFPyju", "0123456711", 2, 2 },
                    { new Guid("30492f01-99d2-4fe6-8a30-884193bb9135"), 0m, new DateTime(2024, 6, 12, 3, 44, 1, 951, DateTimeKind.Utc).AddTicks(7550), "manager3@gmail.com", "Manager 3", "Court", "$2a$11$BN5XoMxElPAHMIRHZFB/QOen5KF27mQd9TmYN0B7vimFqZ0CAev4O", "0123456783", 3, 3 },
                    { new Guid("577d0faa-61f2-4ea6-b7f6-fcc33b94dc2e"), 0m, new DateTime(2024, 6, 12, 3, 44, 2, 454, DateTimeKind.Utc).AddTicks(1179), "customer4@gmail.com", "Customer 4", "Application", "$2a$11$acCdPdU2BSW.irR89llpS.vrC8Ur9kvIlu7P7PNdpDLTR4ddVmZKC", "0123456704", 1, 1 },
                    { new Guid("92856ff8-947d-4e46-87d2-13277a87c0da"), 0m, new DateTime(2024, 6, 12, 3, 44, 2, 577, DateTimeKind.Utc).AddTicks(7857), "customer5@gmail.com", "Customer 5", "Application", "$2a$11$CCpidkOJDq6KA01iu5Yu2O8ErqABLGxonHZigeTLvV1gRadbDwK6O", "0123456705", 1, 3 },
                    { new Guid("b5546fb7-6972-4677-a5a3-a7599d294b1e"), 0m, new DateTime(2024, 6, 12, 3, 44, 2, 79, DateTimeKind.Utc).AddTicks(4260), "customer1@gmail.com", "Customer 1", "Application", "$2a$11$5oW6/WdlUi0kKwq.MwLhmOxY5gcpFknB5f3aKr3QppA4QCjRNLVxu", "0123456701", 1, 2 },
                    { new Guid("bacf040f-4744-4bb7-9689-bc5f846223c1"), 0m, new DateTime(2024, 6, 12, 3, 44, 1, 571, DateTimeKind.Utc).AddTicks(7377), "systemadmin@gmail.com", "Admin", "System", "$2a$11$xsLLOceMsGWCPG6Jgbr1Jud7ZWSYff4uIzHWScbYMGjmLkSeKvTN.", "0123456789", 4, 2 },
                    { new Guid("f0123f28-7d8e-4d44-8358-284ac82f17a9"), 0m, new DateTime(2024, 6, 12, 3, 44, 2, 946, DateTimeKind.Utc).AddTicks(6666), "staff3@gmail.com", "Staff 3", "Court", "$2a$11$kM82CZQczJ8zyxrHdAj/gu.piS6crflc7aoe.KGoL4V.B7Y.VU9Wa", "0123456713", 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourtSchedule_CourtId_DayOfWeek",
                table: "CourtSchedule",
                columns: new[] { "CourtId", "DayOfWeek" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_CreatorId",
                table: "Transaction",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_CreatorId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_CourtSchedule_CourtId_DayOfWeek",
                table: "CourtSchedule");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("012af60d-02b0-4053-be69-bf6264691597"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0b178799-a492-41ac-9d9a-c2a2dafe7dc6"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ddd584f-ce6d-48d8-b55c-edbdb75d9a88"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("159aced1-f67a-4c34-be93-3e0009dd6419"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1a030c5f-4067-4c83-a985-98608f0d7c4f"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("2936edbc-9bad-4549-a3e4-2bfa444feb54"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("30492f01-99d2-4fe6-8a30-884193bb9135"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("577d0faa-61f2-4ea6-b7f6-fcc33b94dc2e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("92856ff8-947d-4e46-87d2-13277a87c0da"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b5546fb7-6972-4677-a5a3-a7599d294b1e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bacf040f-4744-4bb7-9689-bc5f846223c1"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("f0123f28-7d8e-4d44-8358-284ac82f17a9"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CourtEmployee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SlotId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookingMethodId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "IX_CourtSchedule_CourtId",
                table: "CourtSchedule",
                column: "CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_CreatorId",
                table: "Transaction",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
