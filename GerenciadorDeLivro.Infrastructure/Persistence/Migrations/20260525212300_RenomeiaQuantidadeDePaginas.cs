using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeLivro.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenomeiaQuantidadeDePaginas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuatidadeDePaginas",
                table: "Livros",
                newName: "QuantidadeDePaginas");

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaMedia",
                table: "Livros",
                type: "decimal(2,1)",
                precision: 2,
                scale: 1,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)",
                oldPrecision: 2,
                oldScale: 1,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantidadeDePaginas",
                table: "Livros",
                newName: "QuatidadeDePaginas");

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaMedia",
                table: "Livros",
                type: "decimal(2,1)",
                precision: 2,
                scale: 1,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)",
                oldPrecision: 2,
                oldScale: 1);
        }
    }
}
