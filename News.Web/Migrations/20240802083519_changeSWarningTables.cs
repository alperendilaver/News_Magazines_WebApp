using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class changeSWarningTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarningComments_CommentReplies_CommentReplyReplyId",
                table: "WarningComments");

            migrationBuilder.DropForeignKey(
                name: "FK_WarningComments_Comments_CommentId",
                table: "WarningComments");

            migrationBuilder.DropIndex(
                name: "IX_WarningComments_CommentReplyReplyId",
                table: "WarningComments");

            migrationBuilder.DropColumn(
                name: "CommentReplyReplyId",
                table: "WarningComments");

            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "WarningComments");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "WarningComments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WarningComments_Comments_CommentId",
                table: "WarningComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarningComments_Comments_CommentId",
                table: "WarningComments");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "WarningComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommentReplyReplyId",
                table: "WarningComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReplyId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_WarningComments_Comments_CommentId",
                table: "WarningComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId");
        }
    }
}
