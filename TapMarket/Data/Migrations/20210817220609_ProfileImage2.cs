using Microsoft.EntityFrameworkCore.Migrations;

namespace TapMarket.Data.Migrations
{
    public partial class ProfileImage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Listings",
                newName: "ListingImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ListingImage",
                table: "Listings",
                newName: "ImageUrl");
        }
    }
}
