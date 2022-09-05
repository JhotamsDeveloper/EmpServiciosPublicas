using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpServiciosPublicas.Infrastructure.Migrations
{
    public partial class changespqrsdTypeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PQRSDType",
                table: "PQRSDs",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "PQRSDs",
                newName: "PQRSDType");
        }
    }
}
