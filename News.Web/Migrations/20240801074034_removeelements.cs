using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class removeelements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_News_NewId",
                table: "CommentReplies");

            migrationBuilder.DropIndex(
                name: "IX_CommentReplies_NewId",
                table: "CommentReplies");

            migrationBuilder.DropColumn(
                name: "NewId",
                table: "CommentReplies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewId",
                table: "CommentReplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_NewId",
                table: "CommentReplies",
                column: "NewId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_News_NewId",
                table: "CommentReplies",
                column: "NewId",
                principalTable: "News",
                principalColumn: "NewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
