using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrcamentoSaaS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddColunaObservacaoOrcamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "tb_Orcamento",
                type: "nvarchar(1000)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "tb_Orcamento");
        }
    }
}
