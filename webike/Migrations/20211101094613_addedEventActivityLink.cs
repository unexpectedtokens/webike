using Microsoft.EntityFrameworkCore.Migrations;

namespace webike.Migrations
{
    public partial class addedEventActivityLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityEventActivityID",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ActivityEventActivityID",
                table: "Events",
                column: "ActivityEventActivityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventActivities_ActivityEventActivityID",
                table: "Events",
                column: "ActivityEventActivityID",
                principalTable: "EventActivities",
                principalColumn: "EventActivityID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventActivities_ActivityEventActivityID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ActivityEventActivityID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ActivityEventActivityID",
                table: "Events");
        }
    }
}
