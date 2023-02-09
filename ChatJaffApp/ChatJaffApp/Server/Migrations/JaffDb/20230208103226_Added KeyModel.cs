using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatJaffApp.Server.Migrations.JaffDb
{
    /// <inheritdoc />
    public partial class AddedKeyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatKeys",
                columns: table => new
                {
                    ChatRoomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatKeys", x => x.ChatRoomId);
                });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("439f95cc-b2d6-4375-9281-6a5c65ba3806"),
                column: "Sent",
                value: new DateTime(2023, 2, 8, 10, 32, 26, 261, DateTimeKind.Utc).AddTicks(8926));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("abda0517-7354-49fc-9239-8e350b42fb9d"),
                column: "Sent",
                value: new DateTime(2023, 2, 8, 10, 32, 26, 261, DateTimeKind.Utc).AddTicks(8919));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatKeys");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("439f95cc-b2d6-4375-9281-6a5c65ba3806"),
                column: "Sent",
                value: new DateTime(2023, 2, 8, 10, 30, 2, 327, DateTimeKind.Utc).AddTicks(1636));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: new Guid("abda0517-7354-49fc-9239-8e350b42fb9d"),
                column: "Sent",
                value: new DateTime(2023, 2, 8, 10, 30, 2, 327, DateTimeKind.Utc).AddTicks(1629));
        }
    }
}
