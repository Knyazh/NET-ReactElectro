using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroEcommerce.Migrations
{
    public partial class manytomany_productcolor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_ProductId",
                table: "ProductColors");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32bf9dc6-c0d0-4ba5-bed8-b0fc1a16f736"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                columns: new[] { "ProductId", "ColorId" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastName", "Name", "Password", "Role", "UpdatedAt" },
                values: new object[] { new Guid("907440c6-e872-4050-961f-8af398c43105"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", "Heydarov", "Knyaz", "password", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("907440c6-e872-4050-961f-8af398c43105"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "LastName", "Name", "Password", "Role", "UpdatedAt" },
                values: new object[] { new Guid("32bf9dc6-c0d0-4ba5-bed8-b0fc1a16f736"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", "Heydarov", "Knyaz", "password", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductId",
                table: "ProductColors",
                column: "ProductId");
        }
    }
}
