using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fe1787d-3ef1-48e9-a9c0-3dda8c1290e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58737bad-3304-4a81-8d9b-fc3d8d6a201a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8c48efad-3476-415b-9951-22f3c2acfb0c", null, "User", "USER" },
                    { "fa11ab9c-5a44-4db4-b2fa-08833ea4118c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c48efad-3476-415b-9951-22f3c2acfb0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa11ab9c-5a44-4db4-b2fa-08833ea4118c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3fe1787d-3ef1-48e9-a9c0-3dda8c1290e5", null, "User", "USER" },
                    { "58737bad-3304-4a81-8d9b-fc3d8d6a201a", null, "Admin", "ADMIN" }
                });
        }
    }
}
