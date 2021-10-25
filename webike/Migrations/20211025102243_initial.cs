using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webike.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Excercises",
                columns: table => new
                {
                    ExcerciseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorUserID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkoutEventActivityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excercises", x => x.ExcerciseID);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CyclistUserID = table.Column<int>(type: "int", nullable: true),
                    EventID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCoach = table.Column<bool>(type: "bit", nullable: false),
                    WantsToBeCoach = table.Column<bool>(type: "bit", nullable: false),
                    Events = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderUserID = table.Column<int>(type: "int", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contacts_User_SenderUserID",
                        column: x => x.SenderUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventActivities",
                columns: table => new
                {
                    EventActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatotUserID = table.Column<int>(type: "int", nullable: true),
                    RatingsRatingID = table.Column<int>(type: "int", nullable: true),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuitableBikeType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteDifficulty = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventActivities", x => x.EventActivityID);
                    table.ForeignKey(
                        name: "FK_EventActivities_Ratings_RatingsRatingID",
                        column: x => x.RatingsRatingID,
                        principalTable: "Ratings",
                        principalColumn: "RatingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventActivities_User_CreatotUserID",
                        column: x => x.CreatotUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManegerUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_User_ManegerUserID",
                        column: x => x.ManegerUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SenderUserID",
                table: "Contacts",
                column: "SenderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EventActivities_CreatotUserID",
                table: "EventActivities",
                column: "CreatotUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EventActivities_RatingsRatingID",
                table: "EventActivities",
                column: "RatingsRatingID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ManegerUserID",
                table: "Events",
                column: "ManegerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_CreatorUserID",
                table: "Excercises",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_WorkoutEventActivityID",
                table: "Excercises",
                column: "WorkoutEventActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CyclistUserID",
                table: "Ratings",
                column: "CyclistUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EventID",
                table: "Ratings",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_User_EventID",
                table: "User",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_EventActivities_WorkoutEventActivityID",
                table: "Excercises",
                column: "WorkoutEventActivityID",
                principalTable: "EventActivities",
                principalColumn: "EventActivityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_User_CreatorUserID",
                table: "Excercises",
                column: "CreatorUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Events_EventID",
                table: "Ratings",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_User_CyclistUserID",
                table: "Ratings",
                column: "CyclistUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Events_EventID",
                table: "User",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_User_ManegerUserID",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Excercises");

            migrationBuilder.DropTable(
                name: "EventActivities");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
