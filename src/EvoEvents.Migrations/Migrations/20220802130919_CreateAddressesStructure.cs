using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoEvents.Migrations.Migrations
{
    public partial class CreateAddressesStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityLookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityLookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryLookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_CityLookups_CityId",
                        column: x => x.CityId,
                        principalTable: "CityLookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_CountryLookups_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryLookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CityLookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sibiu" },
                    { 2, "Cluj" },
                    { 3, "Bistrita" },
                    { 4, "Frankfurt" },
                    { 5, "Milano" },
                    { 6, "Roma" },
                    { 7, "Hamburg" }
                });

            migrationBuilder.InsertData(
                table: "CountryLookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Romania" },
                    { 2, "Germania" },
                    { 3, "Italia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CityLookups");

            migrationBuilder.DropTable(
                name: "CountryLookups");
        }
    }
}
