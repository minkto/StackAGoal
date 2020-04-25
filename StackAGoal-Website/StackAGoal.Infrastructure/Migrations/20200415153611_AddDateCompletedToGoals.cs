using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackAGoal.Infrastructure.Migrations
{
    public partial class AddDateCompletedToGoals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "Goals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "Goals");
        }
    }
}
