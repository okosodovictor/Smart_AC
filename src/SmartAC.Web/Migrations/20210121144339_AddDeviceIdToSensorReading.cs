using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartAC.Web.Migrations
{
    public partial class AddDeviceIdToSensorReading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorReadings_Devices_DeviceId",
                table: "SensorReadings");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceId",
                table: "SensorReadings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorReadings_Devices_DeviceId",
                table: "SensorReadings",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorReadings_Devices_DeviceId",
                table: "SensorReadings");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceId",
                table: "SensorReadings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorReadings_Devices_DeviceId",
                table: "SensorReadings",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
