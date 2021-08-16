using Microsoft.EntityFrameworkCore.Migrations;

namespace TapMarket.Data.Migrations
{
    public partial class CustomerUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_CustomerId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Favorites",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_CustomerId",
                table: "Favorites",
                newName: "IX_Favorites_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Favorites",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                newName: "IX_Favorites_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_CustomerId",
                table: "Favorites",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
