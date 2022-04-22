using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tham_Backend.Migrations
{
    public partial class AddLikesFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BloggerId",
                table: "Likes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ArticleId",
                table: "Likes",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_BloggerId",
                table: "Likes",
                column: "BloggerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Articles_ArticleId",
                table: "Likes",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Bloggers_BloggerId",
                table: "Likes",
                column: "BloggerId",
                principalTable: "Bloggers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Articles_ArticleId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Bloggers_BloggerId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_ArticleId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_BloggerId",
                table: "Likes");

            migrationBuilder.AlterColumn<int>(
                name: "BloggerId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
