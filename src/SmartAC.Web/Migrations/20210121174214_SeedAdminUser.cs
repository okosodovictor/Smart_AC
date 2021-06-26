using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartAC.Web.Migrations
{
    public partial class SeedAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "PasswordHash" },
                values: new object[] { new Guid("47479da2-7641-445b-9186-15c5e6590ef5"), "admin@gmail.com", "X03MO1qnZdYdgyfeuILPmQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("47479da2-7641-445b-9186-15c5e6590ef5"));
        }
    }
}
