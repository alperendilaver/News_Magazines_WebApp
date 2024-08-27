using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class changeSWarningTabeleseq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarningComments_CommentReplies_CommentReplyReplyId",
                table: "WarningComments");

            migrationBuilder.DropIndex(
                name: "IX_WarningComments_CommentReplyReplyId",
                table: "WarningComments");

            migrationBuilder.DropColumn(
                name: "CommentReplyReplyId",
                table: "WarningComments");

            migrationBuilder.CreateIndex(
                name: "IX_WarningComments_ReplyId",
                table: "WarningComments",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarningComments_CommentReplies_ReplyId",
                table: "WarningComments",
                column: "ReplyId",
                principalTable: "CommentReplies",
                principalColumn: "ReplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarningComments_CommentReplies_ReplyId",
                table: "WarningComments");

            migrationBuilder.DropIndex(
                name: "IX_WarningComments_ReplyId",
                table: "WarningComments");

            migrationBuilder.AddColumn<int>(
                name: "CommentReplyReplyId",
                table: "WarningComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarningComments_CommentReplyReplyId",
                table: "WarningComments",
                column: "CommentReplyReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarningComments_CommentReplies_CommentReplyReplyId",
                table: "WarningComments",
                column: "CommentReplyReplyId",
                principalTable: "CommentReplies",
                principalColumn: "ReplyId");
        }
    }
}
