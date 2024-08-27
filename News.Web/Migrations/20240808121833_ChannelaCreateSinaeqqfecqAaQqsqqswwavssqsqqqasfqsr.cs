using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChannelaCreateSinaeqqfecqAaQqsqqswwavssqsqqqasfqsr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserId",
                table: "Notifies");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserId",
                table: "Notifies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserId",
                table: "Notifies");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserId",
                table: "Notifies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
