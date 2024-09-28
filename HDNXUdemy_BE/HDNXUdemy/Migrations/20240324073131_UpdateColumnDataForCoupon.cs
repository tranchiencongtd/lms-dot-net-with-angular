using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace HDNXUdemyData.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnDataForCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<LocalDateTime>(
                name: "EndDate",
                schema: "sche_dev_HDNXUdemy",
                table: "Coupons",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfCoupon",
                schema: "sche_dev_HDNXUdemy",
                table: "Coupons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<LocalDateTime>(
                name: "StartDate",
                schema: "sche_dev_HDNXUdemy",
                table: "Coupons",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new NodaTime.LocalDateTime(2024, 3, 24, 7, 31, 31).PlusNanoseconds(777831500L), new NodaTime.LocalDateTime(2024, 3, 24, 7, 31, 31).PlusNanoseconds(777831500L) });

            migrationBuilder.UpdateData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new NodaTime.LocalDateTime(2024, 3, 24, 7, 31, 31).PlusNanoseconds(777831500L), new NodaTime.LocalDateTime(2024, 3, 24, 7, 31, 31).PlusNanoseconds(777831500L) });

            migrationBuilder.UpdateData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new NodaTime.LocalDateTime(2024, 3, 24, 7, 31, 31).PlusNanoseconds(777831500L), new NodaTime.LocalDateTime(2024, 3, 24, 7, 31, 31).PlusNanoseconds(777831500L) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "sche_dev_HDNXUdemy",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "NameOfCoupon",
                schema: "sche_dev_HDNXUdemy",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "sche_dev_HDNXUdemy",
                table: "Coupons");

            migrationBuilder.UpdateData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L), new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L) });

            migrationBuilder.UpdateData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L), new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L) });

            migrationBuilder.UpdateData(
                schema: "sche_dev_HDNXUdemy",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L), new NodaTime.LocalDateTime(2024, 3, 23, 14, 39, 0).PlusNanoseconds(598964500L) });
        }
    }
}
