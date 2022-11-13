using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpServiciosPublicas.Infrastructure.Migrations
{
    public partial class ignoreproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("35aef29a-1e7c-4d4d-a2c0-bf582e75a27b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d7730f72-0748-4aca-b821-abc0893581c3"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Availability", "CreatedBy", "CreatedDate", "Descrption", "LastModifiedBy", "LastModifiedDate", "NameIcono", "RouteIcono", "Title", "Url" },
                values: new object[] { new Guid("26ef4d6c-5796-44c0-b56d-8032a7ecb128"), false, null, null, "Esta sessión encontrarás todos los documentos púlicos de interés a la comunidad", null, null, null, null, "Documentos", "documentos" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Availability", "CreatedBy", "CreatedDate", "Descrption", "LastModifiedBy", "LastModifiedDate", "NameIcono", "RouteIcono", "Title", "Url" },
                values: new object[] { new Guid("7b09f267-43f4-4455-b1eb-a2e6300f3895"), false, null, null, "Esta sessión encontrarás toda la información de los eventos, convocatirias e información de interés", null, null, null, null, "Noticias", "noticias" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("26ef4d6c-5796-44c0-b56d-8032a7ecb128"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7b09f267-43f4-4455-b1eb-a2e6300f3895"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Availability", "CreatedBy", "CreatedDate", "Descrption", "LastModifiedBy", "LastModifiedDate", "NameIcono", "RouteIcono", "Title", "Url" },
                values: new object[] { new Guid("35aef29a-1e7c-4d4d-a2c0-bf582e75a27b"), false, null, null, "Esta sessión encontrarás toda la información de los eventos, convocatirias e información de interés", null, null, null, null, "Noticias", "noticias" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Availability", "CreatedBy", "CreatedDate", "Descrption", "LastModifiedBy", "LastModifiedDate", "NameIcono", "RouteIcono", "Title", "Url" },
                values: new object[] { new Guid("d7730f72-0748-4aca-b821-abc0893581c3"), false, null, null, "Esta sessión encontrarás todos los documentos púlicos de interés a la comunidad", null, null, null, null, "Documentos", "documentos" });
        }
    }
}
