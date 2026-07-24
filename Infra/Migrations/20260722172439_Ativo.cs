using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Ativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "UsuarioJogo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "NivelAcesso",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Jogo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "Data" },
                values: new object[] { true, new DateTime(2026, 7, 22, 14, 24, 39, 2, DateTimeKind.Local).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "Data" },
                values: new object[] { true, new DateTime(2026, 7, 22, 14, 24, 39, 2, DateTimeKind.Local).AddTicks(4050) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "UsuarioJogo");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "NivelAcesso");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Jogo");

            migrationBuilder.UpdateData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 1,
                column: "Data",
                value: new DateTime(2026, 7, 16, 20, 22, 34, 88, DateTimeKind.Local).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "NivelAcesso",
                keyColumn: "Id",
                keyValue: 2,
                column: "Data",
                value: new DateTime(2026, 7, 16, 20, 22, 34, 88, DateTimeKind.Local).AddTicks(6915));
        }
    }
}
