using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroEcommerce.Migrations
{
    public partial class addColorSeedingtoDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5ef61a14-9869-4b1e-90e1-c6be7bd37f2b"));

            migrationBuilder.AddColumn<string>(
                name: "ColorPrefix",
                table: "Colors",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "ColorPrefix", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2ff83d6b-6c95-4f7d-9c64-60e406a057a1"), "#ffe4c4", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "bisque", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3e20ac3a-c156-4f60-b0b4-e1f1c205e24d"), "#808080", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "gray", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("45e0cbf9-aa2a-44a4-93a1-bf4d3aa623ce"), "#ffff00", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "yellow", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4b24804b-9a8f-4d33-9f43-8c461e4dbf11"), "#000000", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "black", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6c1d50fc-b6eb-4d76-ba7e-81a7811ea15f"), "#ff0000", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "red", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7a9d04e8-1a22-4aae-8232-62f5a0c28b87"), "#663399", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "rebeccapurple", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8640c057-8997-4b16-b3dd-7c3d3c2e1a12"), "#2f4f4f", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "darkslategray", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8a4c94f8-2437-4e89-9075-56bbcf19c0e9"), "#a52a2a", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "brown", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9a305d7f-5c8f-4fe1-9c0d-d8a8eb4a17c3"), "#ffe4c4", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "bisque", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9c327764-bf90-4b8b-8c38-370cb3aa2a5a"), "#ffffff", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "white", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("af15118a-95ac-487a-b103-c9a0a1918c25"), "#0000ff", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "blue", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c80a742d-12db-4a19-a0e8-44c67f7fb21a"), "#00ced1", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "darkturquoise", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("db1ef4d9-5b95-41a3-8bfb-7f01f8a50f32"), "#008000", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc), "green", new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "ConfirmedDate", "CreatedAt", "Email", "IsAdmin", "IsBanned", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("c13159cd-46a3-4262-9efe-f84fb7583d98"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("2ff83d6b-6c95-4f7d-9c64-60e406a057a1"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("3e20ac3a-c156-4f60-b0b4-e1f1c205e24d"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("45e0cbf9-aa2a-44a4-93a1-bf4d3aa623ce"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("4b24804b-9a8f-4d33-9f43-8c461e4dbf11"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("6c1d50fc-b6eb-4d76-ba7e-81a7811ea15f"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("7a9d04e8-1a22-4aae-8232-62f5a0c28b87"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("8640c057-8997-4b16-b3dd-7c3d3c2e1a12"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("8a4c94f8-2437-4e89-9075-56bbcf19c0e9"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("9a305d7f-5c8f-4fe1-9c0d-d8a8eb4a17c3"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("9c327764-bf90-4b8b-8c38-370cb3aa2a5a"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("af15118a-95ac-487a-b103-c9a0a1918c25"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("c80a742d-12db-4a19-a0e8-44c67f7fb21a"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("db1ef4d9-5b95-41a3-8bfb-7f01f8a50f32"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c13159cd-46a3-4262-9efe-f84fb7583d98"));

            migrationBuilder.DropColumn(
                name: "ColorPrefix",
                table: "Colors");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApplicationPassword", "ConfirmedDate", "CreatedAt", "Email", "IsAdmin", "IsBanned", "IsComfirmed", "LastName", "Name", "Password", "PhoneNumber", "PhysicalImageUrl", "Role", "UpdatedAt", "UserPrefix" },
                values: new object[] { new Guid("5ef61a14-9869-4b1e-90e1-c6be7bd37f2b"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "knyazheydariv@gmail.com", true, false, false, "Heydarov", "Knyaz", "Knyaz123.", "", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
