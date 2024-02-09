using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroEcommerce.Migrations
{
    public partial class superadmin_implement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fbcefecf-7161-4048-8d6d-da2b4e114b66"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "CreatedAt", "Email", "IsAdmin", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("0ba19a19-4f39-4622-833c-0f306d8d64c2"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0ba19a19-4f39-4622-833c-0f306d8d64c2"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "CreatedAt", "Email", "IsAdmin", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("fbcefecf-7161-4048-8d6d-da2b4e114b66"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", false, false, "Heydarov", "Knyaz", "password", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
