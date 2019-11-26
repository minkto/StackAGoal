using Microsoft.EntityFrameworkCore.Migrations;

namespace StackAGoal.Migrations
{
    public partial class AddIconsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Icon_IconId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Icon",
                table: "Icon");

            migrationBuilder.RenameTable(
                name: "Icon",
                newName: "Icons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Icons",
                table: "Icons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Icons_IconId",
                table: "Categories",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Icons_IconId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Icons",
                table: "Icons");

            migrationBuilder.RenameTable(
                name: "Icons",
                newName: "Icon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Icon",
                table: "Icon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Icon_IconId",
                table: "Categories",
                column: "IconId",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
