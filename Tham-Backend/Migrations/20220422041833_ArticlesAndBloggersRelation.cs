using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tham_Backend.Migrations
{
    public partial class ArticlesAndBloggersRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BloggersId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_BloggersId",
                table: "Articles",
                column: "BloggersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Bloggers_BloggersId",
                table: "Articles",
                column: "BloggersId",
                principalTable: "Bloggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Bloggers_BloggersId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_BloggersId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "BloggersId",
                table: "Articles");
        }
    }
}
