using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpServiciosPublicas.Infrastructure.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Storages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Storages_CategoryId",
                table: "Storages",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Categories_CategoryId",
                table: "Storages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Categories_CategoryId",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Storages_CategoryId",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Storages");
        }
    }
}
