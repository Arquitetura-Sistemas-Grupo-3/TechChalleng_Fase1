using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoNivelAcessoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NivelAcessoId",
                table: "Usuario",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NivelAcessoId",
                table: "Usuario",
                column: "NivelAcessoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_NivelAcesso_NivelAcessoId",
                table: "Usuario",
                column: "NivelAcessoId",
                principalTable: "NivelAcesso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_NivelAcesso_NivelAcessoId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_NivelAcessoId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "NivelAcessoId",
                table: "Usuario");
        }
    }
}
