using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartAC.Web.Migrations
{
    public partial class RemoveSecretFromDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Secret",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_Client_SerialNumber",
                table: "Clients",
                newName: "IX_Clients_SerialNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_SerialNumber",
                table: "Client",
                newName: "IX_Client_SerialNumber");

            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "ClientId");
        }
    }
}
