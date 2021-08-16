using Microsoft.EntityFrameworkCore.Migrations;

namespace TapMarket.Data.Migrations
{
    public partial class Content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageContents_Messages_MessageId",
                table: "MessageContents");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "MessageContents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageContents_Messages_MessageId",
                table: "MessageContents",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageContents_Messages_MessageId",
                table: "MessageContents");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "MessageContents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageContents_Messages_MessageId",
                table: "MessageContents",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
