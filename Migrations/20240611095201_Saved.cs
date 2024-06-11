using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class Saved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Facilities_FacilityId",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d4217e8-1dc1-4d31-84ac-8197d26401a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76e6b102-153d-4b6c-ae6d-58dbdceba3bc");

            migrationBuilder.AlterColumn<long>(
                name: "FacilityId",
                table: "Reviews",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Facilities",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37e0766f-4ad3-4e28-82dd-6f8d206d8960", null, "Admin", "ADMIN" },
                    { "7136ea04-2ee2-44a0-9bdf-0f57018f7366", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Facilities_FacilityId",
                table: "Reviews",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Facilities_FacilityId",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37e0766f-4ad3-4e28-82dd-6f8d206d8960");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7136ea04-2ee2-44a0-9bdf-0f57018f7366");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Facilities");

            migrationBuilder.AlterColumn<long>(
                name: "FacilityId",
                table: "Reviews",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d4217e8-1dc1-4d31-84ac-8197d26401a9", null, "Admin", "ADMIN" },
                    { "76e6b102-153d-4b6c-ae6d-58dbdceba3bc", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Facilities_FacilityId",
                table: "Reviews",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
