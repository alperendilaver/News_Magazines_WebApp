using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChannelaCreateSinaeqqfecqAaQqsqswv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReaction_ChannelPosts_PostId",
                table: "PostReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReaction_Users_UserId",
                table: "PostReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostReaction",
                table: "PostReaction");

            migrationBuilder.RenameTable(
                name: "PostReaction",
                newName: "PostReactions");

            migrationBuilder.RenameIndex(
                name: "IX_PostReaction_UserId",
                table: "PostReactions",
                newName: "IX_PostReactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostReaction_PostId",
                table: "PostReactions",
                newName: "IX_PostReactions_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostReactions",
                table: "PostReactions",
                column: "ReactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactions_ChannelPosts_PostId",
                table: "PostReactions",
                column: "PostId",
                principalTable: "ChannelPosts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactions_Users_UserId",
                table: "PostReactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReactions_ChannelPosts_PostId",
                table: "PostReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReactions_Users_UserId",
                table: "PostReactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostReactions",
                table: "PostReactions");

            migrationBuilder.RenameTable(
                name: "PostReactions",
                newName: "PostReaction");

            migrationBuilder.RenameIndex(
                name: "IX_PostReactions_UserId",
                table: "PostReaction",
                newName: "IX_PostReaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostReactions_PostId",
                table: "PostReaction",
                newName: "IX_PostReaction_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostReaction",
                table: "PostReaction",
                column: "ReactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReaction_ChannelPosts_PostId",
                table: "PostReaction",
                column: "PostId",
                principalTable: "ChannelPosts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReaction_Users_UserId",
                table: "PostReaction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
