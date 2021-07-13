using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DCBMS_API.Migrations
{
    public partial class filedAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Patients");
        }
    }
}
