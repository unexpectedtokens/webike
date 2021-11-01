using Microsoft.EntityFrameworkCore.Migrations;

namespace webike.Migrations
{
    public partial class removedroutediff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteDifficulty",
                table: "EventActivities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouteDifficulty",
                table: "EventActivities",
                type: "int",
                nullable: true);
        }
    }
}
