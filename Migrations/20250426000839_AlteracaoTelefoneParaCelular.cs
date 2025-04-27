using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessoLabSystem.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoTelefoneParaCelular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Fornecedores",
                newName: "Celular");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Celular",
                table: "Fornecedores",
                newName: "Telefone");
        }
    }
}
