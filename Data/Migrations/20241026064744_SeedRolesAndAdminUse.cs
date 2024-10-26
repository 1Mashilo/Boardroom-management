using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace boardroom_management.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndAdminUse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4c5b1c3d-f5d6-4e2b-a3f0-314c1be8a7f5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f67ca06-2d62-48f4-b044-e012b95dbf7a", "AQAAAAIAAYagAAAAELoobABG9gmuw5+cqcQu6irs/M5B7+ZiO+laa1by3PBURFNxji3OZMLLkb6Q/Sgx1A==", "b5e20755-824e-43c5-8387-9afe43e7e7c2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4c5b1c3d-f5d6-4e2b-a3f0-314c1be8a7f5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3445711a-225b-4b6f-a5b4-755ef49249fe", "AQAAAAIAAYagAAAAEKHc5tWiDE3H1YMbs5RWmeoh2TEuWw7gTNz7s92nKuhyV/5W57ptOnDm5yrF07hxQQ==", "769237ff-3ea1-4004-abe9-6e34f6313cf1" });
        }
    }
}
