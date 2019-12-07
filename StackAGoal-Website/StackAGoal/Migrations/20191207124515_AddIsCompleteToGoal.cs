using Microsoft.EntityFrameworkCore.Migrations;

namespace StackAGoal.Migrations
{
    public partial class AddIsCompleteToGoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Goals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Goals");
        }
    }
}
