using Microsoft.EntityFrameworkCore.Migrations;

namespace StackAGoal.Migrations
{
    public partial class AddGoalIdToCheckpoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkpoints_Goals_GoalId",
                schema: "dbo",
                table: "Checkpoints");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                schema: "dbo",
                table: "Checkpoints",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkpoints_Goals_GoalId",
                schema: "dbo",
                table: "Checkpoints",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkpoints_Goals_GoalId",
                schema: "dbo",
                table: "Checkpoints");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                schema: "dbo",
                table: "Checkpoints",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Checkpoints_Goals_GoalId",
                schema: "dbo",
                table: "Checkpoints",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
