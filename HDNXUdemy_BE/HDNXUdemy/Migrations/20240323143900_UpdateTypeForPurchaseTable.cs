using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HDNXUdemyData.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTypeForPurchaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheckoutSessionId",
                schema: "sche_dev_HDNXUdemy",
                table: "PurcharseCourses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoupponCode",
                schema: "sche_dev_HDNXUdemy",
                table: "PurcharseCourses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntent",
                schema: "sche_dev_HDNXUdemy",
                table: "PurcharseCourses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionCode",
                schema: "sche_dev_HDNXUdemy",
                table: "PurcharseCourses",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                columns: new[] { "Id", "CreateBy", "CreateDate", "Name", "PictureUrl", "PublicId", "Status", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1L, 1L, new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L), "Công nghệ thông tin", null, null, 0, 1L, new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L) },
                    { 2L, 1L, new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L), "Chat GPT", null, null, 0, 1L, new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L) },
                    { 3L, 1L, new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L), "Khóa học cuộc sống", null, null, 0, 1L, new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DropColumn(
                name: "CheckoutSessionId",
                schema: "sche_dev_HDNXUdemy",
                table: "PurcharseCourses");

            migrationBuilder.DropColumn(
                name: "CoupponCode",
                schema: "sche_dev_HDNXUdemy",
                table: "PurcharseCourses");

            migrationBuilder.DropColumn(
                name: "PaymentIntent",
                schema: "sche_dev_HDNXUdemy",
                table: "PurcharseCourses");

            migrationBuilder.DropColumn(
                name: "PromotionCode",
                schema: "sche_dev_HDNXUdemy",
                table: "PurcharseCourses");
        }
    }
}
