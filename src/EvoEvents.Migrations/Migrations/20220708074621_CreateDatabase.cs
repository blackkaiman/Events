using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoEvents.Migrations.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationVersions",
                columns: new[] { "Id", "Name", "Version" },
                values: new object[] { 1, "Create Skeleton", "1.0.1" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersions_Name",
                table: "ApplicationVersions",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationVersions");
        }
    }
}
