using Microsoft.EntityFrameworkCore.Migrations;

namespace StackAGoal.Migrations
{
    public partial class MakeCheckpointsToGoalsNonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsComplete",
                table: "Goals",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsComplete",
                table: "Goals",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
