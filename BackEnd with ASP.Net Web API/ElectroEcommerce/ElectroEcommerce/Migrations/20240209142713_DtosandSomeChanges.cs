using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroEcommerce.Migrations
{
    public partial class DtosandSomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0fd5389-f630-4c80-ac98-9ecda0441f5c"));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationPassword",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsComfirmed",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalImageUrl",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserPrefix",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "CreatedAt", "Email", "IsAdmin", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("fbcefecf-7161-4048-8d6d-da2b4e114b66"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", false, false, "Heydarov", "Knyaz", "password", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fbcefecf-7161-4048-8d6d-da2b4e114b66"));

            migrationBuilder.DropColumn(
                name: "ApplicationPassword",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsComfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhysicalImageUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserPrefix",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastName", "Name", "Password", "Role", "UpdatedAt" },
                values: new object[] { new Guid("f0fd5389-f630-4c80-ac98-9ecda0441f5c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", "Heydarov", "Knyaz", "password", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
