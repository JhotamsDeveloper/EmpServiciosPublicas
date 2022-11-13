using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpServiciosPublicas.Infrastructure.Migrations
{
    public partial class updatecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Categories_CategoryId",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Storages_CategoryId",
                table: "Storages");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4f4b56f2-9a63-4bac-92ef-d392045d5497"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ca60e712-3ced-4d3d-9749-2e481b612490"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Storages");

            migrationBuilder.RenameColumn(
                name: "Icono",
                table: "Categories",
                newName: "RouteIcono");

            migrationBuilder.AddColumn<string>(
                name: "NameIcono",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Availability", "CreatedBy", "CreatedDate", "Descrption", "LastModifiedBy", "LastModifiedDate", "NameIcono", "RouteIcono", "Title", "Url" },
                values: new object[] { new Guid("35aef29a-1e7c-4d4d-a2c0-bf582e75a27b"), false, null, null, "Esta sessión encontrarás toda la información de los eventos, convocatirias e información de interés", null, null, null, null, "Noticias", "noticias" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Availability", "CreatedBy", "CreatedDate", "Descrption", "LastModifiedBy", "LastModifiedDate", "NameIcono", "RouteIcono", "Title", "Url" },
                values: new object[] { new Guid("d7730f72-0748-4aca-b821-abc0893581c3"), false, null, null, "Esta sessión encontrarás todos los documentos púlicos de interés a la comunidad", null, null, null, null, "Documentos", "documentos" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("35aef29a-1e7c-4d4d-a2c0-bf582e75a27b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d7730f72-0748-4aca-b821-abc0893581c3"));

            migrationBuilder.DropColumn(
                name: "NameIcono",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "RouteIcono",
                table: "Categories",
                newName: "Icono");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Storages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Availability", "CreatedBy", "CreatedDate", "Descrption", "Icono", "LastModifiedBy", "LastModifiedDate", "Title", "Url" },
                values: new object[] { new Guid("4f4b56f2-9a63-4bac-92ef-d392045d5497"), false, null, null, "Esta sessión encontrarás toda la información de los eventos, convocatirias e información de interés", null, null, null, "Noticias", "noticias" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Availability", "CreatedBy", "CreatedDate", "Descrption", "Icono", "LastModifiedBy", "LastModifiedDate", "Title", "Url" },
                values: new object[] { new Guid("ca60e712-3ced-4d3d-9749-2e481b612490"), false, null, null, "Esta sessión encontrarás todos los documentos púlicos de interés a la comunidad", null, null, null, "Documentos", "documentos" });

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
    }
}
