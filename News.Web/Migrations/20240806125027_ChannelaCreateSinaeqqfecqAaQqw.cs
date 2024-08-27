using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChannelaCreateSinaeqqfecqAaQqw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelPosts_Channels_PostId",
                table: "ChannelPosts");

            migrationBuilder.RenameColumn(
                name: "CnId",
                table: "ChannelPosts",
                newName: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelPosts_ChannelId",
                table: "ChannelPosts",
                column: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelPosts_Channels_ChannelId",
                table: "ChannelPosts",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelPosts_Channels_ChannelId",
                table: "ChannelPosts");

            migrationBuilder.DropIndex(
                name: "IX_ChannelPosts_ChannelId",
                table: "ChannelPosts");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "ChannelPosts",
                newName: "CnId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelPosts_Channels_PostId",
                table: "ChannelPosts",
                column: "PostId",
                principalTable: "Channels",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
