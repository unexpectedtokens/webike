using Microsoft.EntityFrameworkCore.Migrations;

namespace webike.Migrations
{
    public partial class fixedRatingRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventActivities_Ratings_RatingsRatingID",
                table: "EventActivities");

            migrationBuilder.DropIndex(
                name: "IX_EventActivities_RatingsRatingID",
                table: "EventActivities");

            migrationBuilder.DropColumn(
                name: "RatingsRatingID",
                table: "EventActivities");

            migrationBuilder.AddColumn<int>(
                name: "EventActivityID",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EventActivityID",
                table: "Ratings",
                column: "EventActivityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_EventActivities_EventActivityID",
                table: "Ratings",
                column: "EventActivityID",
                principalTable: "EventActivities",
                principalColumn: "EventActivityID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_EventActivities_EventActivityID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_EventActivityID",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "EventActivityID",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "RatingsRatingID",
                table: "EventActivities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventActivities_RatingsRatingID",
                table: "EventActivities",
                column: "RatingsRatingID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventActivities_Ratings_RatingsRatingID",
                table: "EventActivities",
                column: "RatingsRatingID",
                principalTable: "Ratings",
                principalColumn: "RatingID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
