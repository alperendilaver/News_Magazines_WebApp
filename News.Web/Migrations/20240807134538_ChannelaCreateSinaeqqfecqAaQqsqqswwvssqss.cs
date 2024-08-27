using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChannelaCreateSinaeqqfecqAaQqsqqswwvssqss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelPosts_Channels_ChannelId",
                table: "ChannelPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ChannelUser_Users_UserId",
                table: "ChannelUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelPosts_Channels_ChannelId",
                table: "ChannelPosts",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelUser_Users_UserId",
                table: "ChannelUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelPosts_Channels_ChannelId",
                table: "ChannelPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ChannelUser_Users_UserId",
                table: "ChannelUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelPosts_Channels_ChannelId",
                table: "ChannelPosts",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelUser_Users_UserId",
                table: "ChannelUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
