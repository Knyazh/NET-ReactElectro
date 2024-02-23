using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroEcommerce.Migrations
{
    public partial class orderrelationshipdone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5e2f5bba-4dd6-4af9-b986-82c1eabb35ff"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderItemPrefix",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProductColorName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ColorIDs",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "ProductSizeName",
                table: "Items",
                newName: "ProductPrefix");

            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "Items",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ProductOrderPhoto",
                table: "Items",
                newName: "PhisicalImageName");

            migrationBuilder.AddColumn<string>(
                name: "CurrentOrderStatus",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotalPrice",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderItemSinglePrice",
                table: "Items",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderItemTotalPrice",
                table: "Items",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductColorID",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductID",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ColorID",
                table: "BasketItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "ConfirmedDate", "CreatedAt", "Email", "IsAdmin", "IsBanned", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("e1b0eb36-5376-47a6-8a2c-46b6d5f7bfee"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e1b0eb36-5376-47a6-8a2c-46b6d5f7bfee"));

            migrationBuilder.DropColumn(
                name: "CurrentOrderStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTotalPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderItemSinglePrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OrderItemTotalPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProductColorID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ColorID",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Items",
                newName: "ProductQuantity");

            migrationBuilder.RenameColumn(
                name: "ProductPrefix",
                table: "Items",
                newName: "ProductSizeName");

            migrationBuilder.RenameColumn(
                name: "PhisicalImageName",
                table: "Items",
                newName: "ProductOrderPhoto");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderItemPrefix",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductColorName",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid[]>(
                name: "ColorIDs",
                table: "BasketItems",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "ConfirmedDate", "CreatedAt", "Email", "IsAdmin", "IsBanned", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("5e2f5bba-4dd6-4af9-b986-82c1eabb35ff"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
