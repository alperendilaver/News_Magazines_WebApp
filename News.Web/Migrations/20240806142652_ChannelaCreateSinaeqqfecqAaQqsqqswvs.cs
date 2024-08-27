using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChannelaCreateSinaeqqfecqAaQqsqqswvs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReactions_ChannelPosts_PostId",
                table: "PostReactions");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactions_ChannelPosts_PostId",
                table: "PostReactions",
                column: "PostId",
                principalTable: "ChannelPosts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReactions_ChannelPosts_PostId",
                table: "PostReactions");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactions_ChannelPosts_PostId",
                table: "PostReactions",
                column: "PostId",
                principalTable: "ChannelPosts",
                principalColumn: "PostId");
        }
    }
}
