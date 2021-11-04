using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webike.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    ReceiverUserID = table.Column<int>(type: "int", nullable: true),
                    SenderUserID = table.Column<int>(type: "int", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contacts_User_ReceiverUserID",
                        column: x => x.ReceiverUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
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
                    CreatorUserID = table.Column<int>(type: "int", nullable: true),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    SuitableBikeType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventActivities", x => x.EventActivityID);
                    table.ForeignKey(
                        name: "FK_EventActivities_User_CreatorUserID",
                        column: x => x.CreatorUserID,
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
                    ManagerID = table.Column<int>(type: "int", nullable: true),
                    Public = table.Column<bool>(type: "bit", nullable: false),
                    ActivityEventActivityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_EventActivities_ActivityEventActivityID",
                        column: x => x.ActivityEventActivityID,
                        principalTable: "EventActivities",
                        principalColumn: "EventActivityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_User_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

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
                    table.ForeignKey(
                        name: "FK_Excercises_EventActivities_WorkoutEventActivityID",
                        column: x => x.WorkoutEventActivityID,
                        principalTable: "EventActivities",
                        principalColumn: "EventActivityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Excercises_User_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CyclistEvent",
                columns: table => new
                {
                    AttendeesUserID = table.Column<int>(type: "int", nullable: false),
                    EventsPartOfEventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CyclistEvent", x => new { x.AttendeesUserID, x.EventsPartOfEventID });
                    table.ForeignKey(
                        name: "FK_CyclistEvent_Events_EventsPartOfEventID",
                        column: x => x.EventsPartOfEventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CyclistEvent_User_AttendeesUserID",
                        column: x => x.AttendeesUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
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
                    EventActivityID = table.Column<int>(type: "int", nullable: true),
                    EventID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Ratings_EventActivities_EventActivityID",
                        column: x => x.EventActivityID,
                        principalTable: "EventActivities",
                        principalColumn: "EventActivityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_User_CyclistUserID",
                        column: x => x.CyclistUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ReceiverUserID",
                table: "Contacts",
                column: "ReceiverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SenderUserID",
                table: "Contacts",
                column: "SenderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CyclistEvent_EventsPartOfEventID",
                table: "CyclistEvent",
                column: "EventsPartOfEventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventActivities_CreatorUserID",
                table: "EventActivities",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ActivityEventActivityID",
                table: "Events",
                column: "ActivityEventActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ManagerID",
                table: "Events",
                column: "ManagerID");

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
                name: "IX_Ratings_EventActivityID",
                table: "Ratings",
                column: "EventActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EventID",
                table: "Ratings",
                column: "EventID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "CyclistEvent");

            migrationBuilder.DropTable(
                name: "Excercises");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventActivities");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
