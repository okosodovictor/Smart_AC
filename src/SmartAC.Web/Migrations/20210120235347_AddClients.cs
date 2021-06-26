using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartAC.Web.Migrations
{
    public partial class AddClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    Secret = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ClientId", "Secret", "SerialNumber" },
                values: new object[,]
                {
                    { new Guid("8bd99aa7-539a-46f3-821b-dfce8ec57762"), "a79b6fc9-3884-4232-84ef-0bd8a687fa12", "Device-001" },
                    { new Guid("69aef910-b87e-415a-91d2-8bed0f54c545"), "b17a76fc-5754-4422-8aa5-7c9b0db4401f", "Device-002" },
                    { new Guid("2244f0ba-6e36-4b78-813f-021c749bfe47"), "5733e2b1-bab9-442a-b671-59423e7a2b4c", "Device-003" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_SerialNumber",
                table: "Client",
                column: "SerialNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
