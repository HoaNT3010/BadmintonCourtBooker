using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityCourt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtEmployee_User_UserId",
                table: "CourtEmployee");

            migrationBuilder.DropIndex(
                name: "IX_CourtEmployee_CourtId",
                table: "CourtEmployee");

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

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CourtPaymentMethod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Court",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BookingMethods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BookingTime", "CreatedDate", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role", "Status" },
                values: new object[,]
                {
                    { new Guid("17327a7d-3548-49a8-a23e-991a9735784c"), 0m, new DateTime(2024, 7, 1, 13, 2, 27, 870, DateTimeKind.Utc).AddTicks(3927), "systemadmin@gmail.com", "Admin", "System", "$2a$11$WFDRej.nZ0qe76J/SAOfG.o8pJfHRyK0BkXjGSTPqWIWMmze5fVCe", "0123456789", 4, 2 },
                    { new Guid("1a49e9da-205a-40c0-ac81-a49dd3efedd8"), 0m, new DateTime(2024, 7, 1, 13, 2, 28, 756, DateTimeKind.Utc).AddTicks(1502), "customer4@gmail.com", "Customer 4", "Application", "$2a$11$h2GShZc2cwutHLCuqHd.0.GE5EBvhlnapRs7QGtL9B5RlJaVJbJBO", "0123456704", 1, 1 },
                    { new Guid("1f941352-038f-47c9-9341-f4be88164241"), 0m, new DateTime(2024, 7, 1, 13, 2, 29, 134, DateTimeKind.Utc).AddTicks(9598), "staff2@gmail.com", "Staff 2", "Court", "$2a$11$AloQhZVPqOj7RuSuWgAspOrk3H/DFVkXYW10U7VRZslfV1wes4mlK", "0123456712", 2, 2 },
                    { new Guid("4c1e1cd7-5bfc-46d7-8e28-b3d76da615f3"), 0m, new DateTime(2024, 7, 1, 13, 2, 28, 631, DateTimeKind.Utc).AddTicks(6919), "customer3@gmail.com", "Customer 3", "Application", "$2a$11$NMefz0EIQW0wI0m1ELK7v.N/H5cnYsVsEClMaepVOnHfM4a0xc7V6", "0123456703", 1, 2 },
                    { new Guid("4cdf2092-7c95-4b16-83f1-cfd5cb75a7d3"), 0m, new DateTime(2024, 7, 1, 13, 2, 29, 263, DateTimeKind.Utc).AddTicks(4187), "staff3@gmail.com", "Staff 3", "Court", "$2a$11$6mtn5x599mMXPzWBxHKvWu7R.MUt3gJcJuCFzWcGsWZyxCbdmcNWm", "0123456713", 2, 3 },
                    { new Guid("60aaf842-9a5d-4b40-a249-9d141e86903f"), 0m, new DateTime(2024, 7, 1, 13, 2, 28, 255, DateTimeKind.Utc).AddTicks(8156), "manager3@gmail.com", "Manager 3", "Court", "$2a$11$9GrmVp3GCxVaE3KQa/IENuof44Vz04s42a/GkaMe/1XK/h4iCrX7.", "0123456783", 3, 3 },
                    { new Guid("85d513a0-237e-427b-a85a-8e2b6ea2ccc8"), 0m, new DateTime(2024, 7, 1, 13, 2, 28, 128, DateTimeKind.Utc).AddTicks(9291), "manager2@gmail.com", "Manager 2", "Court", "$2a$11$O36qHz4B5cX22aHxpeMIXuip3CGfTJne4gJ3LBN6AOu2fCFIpgXEa", "0123456782", 3, 2 },
                    { new Guid("b090d6bb-5bd4-45fe-a4f3-9d96fdf44f0b"), 0m, new DateTime(2024, 7, 1, 13, 2, 29, 9, DateTimeKind.Utc).AddTicks(5974), "staff1@gmail.com", "Staff 1", "Court", "$2a$11$esdcKBye8uMj0UohFqlieODA4rmxUqYs1o4SrgFla6/4RVOM9/P9y", "0123456711", 2, 2 },
                    { new Guid("ccf0c477-6d32-48bf-be34-3e93410c656a"), 0m, new DateTime(2024, 7, 1, 13, 2, 28, 880, DateTimeKind.Utc).AddTicks(4166), "customer5@gmail.com", "Customer 5", "Application", "$2a$11$W6eirp7tw3oH6OIpRGtfNuHegKIDo3crEMC6aFt8619reFnINtw9u", "0123456705", 1, 3 },
                    { new Guid("d8460477-60c5-4f3e-9034-1e2b8db6075b"), 0m, new DateTime(2024, 7, 1, 13, 2, 28, 383, DateTimeKind.Utc).AddTicks(2407), "customer1@gmail.com", "Customer 1", "Application", "$2a$11$O3GuOsDzRs18VHwIYvatB.0rclEVHY/7asrnU1mCu.RwD3PG3395W", "0123456701", 1, 2 },
                    { new Guid("db0a1cf5-e247-457c-b84b-cee95dd1ab1b"), 0m, new DateTime(2024, 7, 1, 13, 2, 27, 997, DateTimeKind.Utc).AddTicks(4287), "manager1@gmail.com", "Manager 1", "Court", "$2a$11$ho.lsTek7FGAJpSYqaRfpeIxsXzik2PfhRxSt5AmmbzhD0Sj6g4T.", "0123456781", 3, 2 },
                    { new Guid("db1cfe23-7e71-4198-8f3e-c7facf5e70e5"), 0m, new DateTime(2024, 7, 1, 13, 2, 28, 507, DateTimeKind.Utc).AddTicks(9776), "customer2@gmail.com", "Customer 2", "Application", "$2a$11$v7rg1j6zlbYF1uEIgH4ILeeAKIgg6x.MQKfOwVM1tWDY9rJAr6Isq", "0123456702", 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourtEmployee_CourtId_UserId",
                table: "CourtEmployee",
                columns: new[] { "CourtId", "UserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtEmployee_User_UserId",
                table: "CourtEmployee",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtEmployee_User_UserId",
                table: "CourtEmployee");

            migrationBuilder.DropIndex(
                name: "IX_CourtEmployee_CourtId_UserId",
                table: "CourtEmployee");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("17327a7d-3548-49a8-a23e-991a9735784c"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1a49e9da-205a-40c0-ac81-a49dd3efedd8"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1f941352-038f-47c9-9341-f4be88164241"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("4c1e1cd7-5bfc-46d7-8e28-b3d76da615f3"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("4cdf2092-7c95-4b16-83f1-cfd5cb75a7d3"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("60aaf842-9a5d-4b40-a249-9d141e86903f"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("85d513a0-237e-427b-a85a-8e2b6ea2ccc8"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b090d6bb-5bd4-45fe-a4f3-9d96fdf44f0b"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ccf0c477-6d32-48bf-be34-3e93410c656a"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d8460477-60c5-4f3e-9034-1e2b8db6075b"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("db0a1cf5-e247-457c-b84b-cee95dd1ab1b"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("db1cfe23-7e71-4198-8f3e-c7facf5e70e5"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CourtPaymentMethod");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BookingMethods");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Court",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
                name: "IX_CourtEmployee_CourtId",
                table: "CourtEmployee",
                column: "CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtEmployee_User_UserId",
                table: "CourtEmployee",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
