using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fltrni.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceProviderUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ServiceProviders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_UserId",
                table: "ServiceProviders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviders_AspNetUsers_UserId",
                table: "ServiceProviders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_AspNetUsers_UserId",
                table: "ServiceProviders");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_UserId",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ServiceProviders");
        }
    }
}
