using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoEvents.Migrations.Migrations
{
    public partial class UpdateEventsWithDateInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getDate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getDate()");

            migrationBuilder.InsertData(
                table: "EventTypeLookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 0, "None" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypeLookups",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "Events");
        }
    }
}
