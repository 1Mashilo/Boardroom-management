using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace boardroom_management.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "208a99c0-04b3-41b2-8557-8e2a3f2bfcbe", null, "Admin", "ADMIN" },
                    { "3e382f61-b407-4e4c-b769-50cd04578451", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4c5b1c3d-f5d6-4e2b-a3f0-314c1be8a7f5", 0, "3445711a-225b-4b6f-a5b4-755ef49249fe", "admin@boardroom.com", true, "Default", "Admin", false, null, "ADMIN@BOARDROOM.COM", "ADMIN@BOARDROOM.COM", "AQAAAAIAAYagAAAAEKHc5tWiDE3H1YMbs5RWmeoh2TEuWw7gTNz7s92nKuhyV/5W57ptOnDm5yrF07hxQQ==", null, false, "769237ff-3ea1-4004-abe9-6e34f6313cf1", false, "admin@boardroom.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "208a99c0-04b3-41b2-8557-8e2a3f2bfcbe", "4c5b1c3d-f5d6-4e2b-a3f0-314c1be8a7f5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e382f61-b407-4e4c-b769-50cd04578451");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "208a99c0-04b3-41b2-8557-8e2a3f2bfcbe", "4c5b1c3d-f5d6-4e2b-a3f0-314c1be8a7f5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "208a99c0-04b3-41b2-8557-8e2a3f2bfcbe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4c5b1c3d-f5d6-4e2b-a3f0-314c1be8a7f5");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
