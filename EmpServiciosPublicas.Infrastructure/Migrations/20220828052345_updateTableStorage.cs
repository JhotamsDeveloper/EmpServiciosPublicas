using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpServiciosPublicas.Infrastructure.Migrations
{
    public partial class updateTableStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Biddings_BiddingId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Posts_PostId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_PQRSDs_PqrsdId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_TenderProposals_TenderProposalId",
                table: "Storages");

            migrationBuilder.AlterColumn<int>(
                name: "TenderProposalId",
                table: "Storages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RouteFile",
                table: "Storages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Storages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PqrsdId",
                table: "Storages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Storages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameFile",
                table: "Storages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BiddingId",
                table: "Storages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Biddings_BiddingId",
                table: "Storages",
                column: "BiddingId",
                principalTable: "Biddings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Posts_PostId",
                table: "Storages",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_PQRSDs_PqrsdId",
                table: "Storages",
                column: "PqrsdId",
                principalTable: "PQRSDs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_TenderProposals_TenderProposalId",
                table: "Storages",
                column: "TenderProposalId",
                principalTable: "TenderProposals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Biddings_BiddingId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Posts_PostId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_PQRSDs_PqrsdId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_TenderProposals_TenderProposalId",
                table: "Storages");

            migrationBuilder.AlterColumn<int>(
                name: "TenderProposalId",
                table: "Storages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RouteFile",
                table: "Storages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Storages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PqrsdId",
                table: "Storages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Storages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameFile",
                table: "Storages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "BiddingId",
                table: "Storages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Biddings_BiddingId",
                table: "Storages",
                column: "BiddingId",
                principalTable: "Biddings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Posts_PostId",
                table: "Storages",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_PQRSDs_PqrsdId",
                table: "Storages",
                column: "PqrsdId",
                principalTable: "PQRSDs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_TenderProposals_TenderProposalId",
                table: "Storages",
                column: "TenderProposalId",
                principalTable: "TenderProposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
