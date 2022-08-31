using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoEvents.Migrations.Migrations
{
    public partial class CreateStructureForEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventTypeLookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypeLookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MaxNoAttendees = table.Column<int>(type: "int", nullable: false),
                    EventTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventTypeLookups_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypeLookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventTypeLookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Movie" });

            migrationBuilder.InsertData(
                table: "EventTypeLookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Concert" });

            migrationBuilder.InsertData(
                table: "EventTypeLookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Talk" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventTypeLookups");
        }
    }
}
