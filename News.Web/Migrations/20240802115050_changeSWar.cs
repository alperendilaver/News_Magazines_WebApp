using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class changeSWar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarningComments_CommentReplies_ReplyId",
                table: "WarningComments");

            migrationBuilder.AddForeignKey(
                name: "FK_WarningComments_CommentReplies_ReplyId",
                table: "WarningComments",
                column: "ReplyId",
                principalTable: "CommentReplies",
                principalColumn: "ReplyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarningComments_CommentReplies_ReplyId",
                table: "WarningComments");

            migrationBuilder.AddForeignKey(
                name: "FK_WarningComments_CommentReplies_ReplyId",
                table: "WarningComments",
                column: "ReplyId",
                principalTable: "CommentReplies",
                principalColumn: "ReplyId");
        }
    }
}
