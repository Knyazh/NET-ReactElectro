using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroEcommerce.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CurrentCategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d5d5f7b1-aa74-4984-9a01-ea625d85ba1f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentCategoryId",
                table: "Products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "ConfirmedDate", "CreatedAt", "Email", "IsAdmin", "IsBanned", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("4a158a6f-9b42-4e56-aa78-0fd389ab90d1"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CurrentCategoryId",
                table: "Products",
                column: "CurrentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CurrentCategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4a158a6f-9b42-4e56-aa78-0fd389ab90d1"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentCategoryId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "ConfirmedDate", "CreatedAt", "Email", "IsAdmin", "IsBanned", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("d5d5f7b1-aa74-4984-9a01-ea625d85ba1f"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CurrentCategoryId",
                table: "Products",
                column: "CurrentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
