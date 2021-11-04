using Microsoft.EntityFrameworkCore.Migrations;

namespace webike.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_User_ManagerID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Excercises_EventActivities_WorkoutEventActivityID",
                table: "Excercises");

            migrationBuilder.DropIndex(
                name: "IX_Excercises_WorkoutEventActivityID",
                table: "Excercises");

            migrationBuilder.DropColumn(
                name: "WorkoutEventActivityID",
                table: "Excercises");

            migrationBuilder.CreateTable(
                name: "ExcerciseWorkout",
                columns: table => new
                {
                    ExcercisesExcerciseID = table.Column<int>(type: "int", nullable: false),
                    WorkoutsPartOfEventActivityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcerciseWorkout", x => new { x.ExcercisesExcerciseID, x.WorkoutsPartOfEventActivityID });
                    table.ForeignKey(
                        name: "FK_ExcerciseWorkout_EventActivities_WorkoutsPartOfEventActivityID",
                        column: x => x.WorkoutsPartOfEventActivityID,
                        principalTable: "EventActivities",
                        principalColumn: "EventActivityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcerciseWorkout_Excercises_ExcercisesExcerciseID",
                        column: x => x.ExcercisesExcerciseID,
                        principalTable: "Excercises",
                        principalColumn: "ExcerciseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcerciseWorkout_WorkoutsPartOfEventActivityID",
                table: "ExcerciseWorkout",
                column: "WorkoutsPartOfEventActivityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_User_ManagerID",
                table: "Events",
                column: "ManagerID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_User_ManagerID",
                table: "Events");

            migrationBuilder.DropTable(
                name: "ExcerciseWorkout");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutEventActivityID",
                table: "Excercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_WorkoutEventActivityID",
                table: "Excercises",
                column: "WorkoutEventActivityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_User_ManagerID",
                table: "Events",
                column: "ManagerID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_EventActivities_WorkoutEventActivityID",
                table: "Excercises",
                column: "WorkoutEventActivityID",
                principalTable: "EventActivities",
                principalColumn: "EventActivityID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
