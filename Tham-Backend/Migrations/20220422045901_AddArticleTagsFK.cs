using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tham_Backend.Migrations
{
    public partial class AddArticleTagsFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_ArticleId",
                table: "ArticleTags",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Articles_ArticleId",
                table: "ArticleTags",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tags_TagId",
                table: "ArticleTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Articles_ArticleId",
                table: "ArticleTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tags_TagId",
                table: "ArticleTags");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTags_ArticleId",
                table: "ArticleTags");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags");
        }
    }
}
