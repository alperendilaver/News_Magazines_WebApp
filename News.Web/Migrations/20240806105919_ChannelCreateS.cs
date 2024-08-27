using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChannelCreateS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Authors_AuthorId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Channels_PostId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReaction_Post_PostId",
                table: "PostReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "ChannelPosts");

            migrationBuilder.RenameIndex(
                name: "IX_Post_AuthorId",
                table: "ChannelPosts",
                newName: "IX_ChannelPosts_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChannelPosts",
                table: "ChannelPosts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelPosts_Authors_AuthorId",
                table: "ChannelPosts",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelPosts_Channels_PostId",
                table: "ChannelPosts",
                column: "PostId",
                principalTable: "Channels",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReaction_ChannelPosts_PostId",
                table: "PostReaction",
                column: "PostId",
                principalTable: "ChannelPosts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelPosts_Authors_AuthorId",
                table: "ChannelPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ChannelPosts_Channels_PostId",
                table: "ChannelPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReaction_ChannelPosts_PostId",
                table: "PostReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChannelPosts",
                table: "ChannelPosts");

            migrationBuilder.RenameTable(
                name: "ChannelPosts",
                newName: "Post");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelPosts_AuthorId",
                table: "Post",
                newName: "IX_Post_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Authors_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Channels_PostId",
                table: "Post",
                column: "PostId",
                principalTable: "Channels",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReaction_Post_PostId",
                table: "PostReaction",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
