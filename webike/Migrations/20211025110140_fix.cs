using Microsoft.EntityFrameworkCore.Migrations;

namespace webike.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventActivities_User_CreatotUserID",
                table: "EventActivities");

            migrationBuilder.RenameColumn(
                name: "CreatotUserID",
                table: "EventActivities",
                newName: "CreatorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_EventActivities_CreatotUserID",
                table: "EventActivities",
                newName: "IX_EventActivities_CreatorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventActivities_User_CreatorUserID",
                table: "EventActivities",
                column: "CreatorUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventActivities_User_CreatorUserID",
                table: "EventActivities");

            migrationBuilder.RenameColumn(
                name: "CreatorUserID",
                table: "EventActivities",
                newName: "CreatotUserID");

            migrationBuilder.RenameIndex(
                name: "IX_EventActivities_CreatorUserID",
                table: "EventActivities",
                newName: "IX_EventActivities_CreatotUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventActivities_User_CreatotUserID",
                table: "EventActivities",
                column: "CreatotUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
