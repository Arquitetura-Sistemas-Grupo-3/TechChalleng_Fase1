using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 1,
                column: "Data",
                value: new DateTime(2026, 8, 24, 23, 33, 59, 555, DateTimeKind.Local).AddTicks(6992));

            migrationBuilder.UpdateData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 2,
                column: "Data",
                value: new DateTime(2026, 8, 24, 23, 33, 59, 555, DateTimeKind.Local).AddTicks(7006));

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Ativo", "Data", "Email", "NivelAcessoId", "Nome", "Senha" },
                values: new object[] { 1, true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 1, "admin", "$2a$11$/pStnQtIExzkwxiUM5r5yOmT972StJfW5M.j34r4xwPNerS6mSStC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 1,
                column: "Data",
                value: new DateTime(2026, 7, 22, 14, 24, 39, 2, DateTimeKind.Local).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 2,
                column: "Data",
                value: new DateTime(2026, 7, 22, 14, 24, 39, 2, DateTimeKind.Local).AddTicks(4050));
        }
    }
}
