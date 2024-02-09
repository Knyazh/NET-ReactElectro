using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroEcommerce.Migrations
{
    public partial class reletaionorder_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55120420-48fd-4b32-8fd3-35a31ece6f8a"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastName", "Name", "Password", "Role", "UpdatedAt" },
                values: new object[] { new Guid("32bf9dc6-c0d0-4ba5-bed8-b0fc1a16f736"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", "Heydarov", "Knyaz", "password", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32bf9dc6-c0d0-4ba5-bed8-b0fc1a16f736"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastName", "Name", "Password", "Role", "UpdatedAt" },
                values: new object[] { new Guid("55120420-48fd-4b32-8fd3-35a31ece6f8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", "Heydarov", "Knyaz", "password", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
