using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.DataAccess.Migrations
{
    public partial class ChangeColumnNameInMenuItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_DisplayId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_DisplayId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "DisplayId",
                table: "MenuItems");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems");

            migrationBuilder.AddColumn<int>(
                name: "DisplayId",
                table: "MenuItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_DisplayId",
                table: "MenuItems",
                column: "DisplayId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_DisplayId",
                table: "MenuItems",
                column: "DisplayId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
