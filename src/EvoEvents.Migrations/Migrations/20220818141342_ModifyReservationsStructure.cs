using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoEvents.Migrations.Migrations
{
    public partial class ModifyReservationsStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccompanyingPersonEmail",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "AccompanyingPersonId",
                table: "Reservations",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccompanyingPersonId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "AccompanyingPersonEmail",
                table: "Reservations",
                type: "nvarchar(74)",
                maxLength: 74,
                nullable: true);
        }
    }
}
