using Microsoft.EntityFrameworkCore.Migrations;

namespace StackAGoal.Data.Migrations
{
    public partial class AddedCategoryAssociationToGoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Goals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_CategoryId",
                table: "Goals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Categories_CategoryId",
                table: "Goals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Categories_CategoryId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_CategoryId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Goals");
        }
    }
}
