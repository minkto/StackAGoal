using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackAGoal.Migrations
{
    public partial class AddedIconsToCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Icon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageURL = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icon", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IconId",
                table: "Categories",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Icon_IconId",
                table: "Categories",
                column: "IconId",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Icon_IconId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Icon");

            migrationBuilder.DropIndex(
                name: "IX_Categories_IconId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Categories");
        }
    }
}
