using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class SeedNiveisAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NivelAcesso",
                columns: new[] { "Id", "Data", "Nome" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 16, 20, 22, 34, 88, DateTimeKind.Local).AddTicks(6900), "Admin" },
                    { 2, new DateTime(2026, 7, 16, 20, 22, 34, 88, DateTimeKind.Local).AddTicks(6915), "Usuário" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
