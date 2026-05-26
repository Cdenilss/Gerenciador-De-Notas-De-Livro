using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeLivro.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlteraCapaLivroParaBytes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "CapaLivro",
                table: "Livros",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "CapaLivro",
                table: "Livros",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
