using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroEcommerce.Migrations
{
    public partial class brandcontroller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4104d574-c5d2-4ff1-99c6-c628bcd6c995"));

            migrationBuilder.DropColumn(
                name: "PyshicalImageName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Since",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "BrandCode",
                table: "Brands",
                newName: "LogoURL");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "PyshicalImageNames",
                table: "Products",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandPrefix",
                table: "Brands",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "ConfirmedDate", "CreatedAt", "Email", "IsAdmin", "IsBanned", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("5ef61a14-9869-4b1e-90e1-c6be7bd37f2b"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5ef61a14-9869-4b1e-90e1-c6be7bd37f2b"));

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PyshicalImageNames",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandPrefix",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "LogoURL",
                table: "Brands",
                newName: "BrandCode");

            migrationBuilder.AddColumn<string>(
                name: "PyshicalImageName",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Since",
                table: "Brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "ConfirmedDate", "CreatedAt", "Email", "IsAdmin", "IsBanned", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("4104d574-c5d2-4ff1-99c6-c628bcd6c995"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
