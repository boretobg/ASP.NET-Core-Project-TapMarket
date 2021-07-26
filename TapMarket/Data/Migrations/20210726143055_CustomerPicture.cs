using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapMarket.Data.Migrations
{
    public partial class CustomerPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Customers");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePic",
                table: "Customers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
