using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrcamentoSaaS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexOrcamentoClienteDetalhes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "tb_ItemOrcamento",
                newName: "ValorItem");

            migrationBuilder.RenameColumn(
                name: "Desconto",
                table: "tb_ItemOrcamento",
                newName: "DescontoItem");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_Tenant_Cliente_Codigo",
                table: "tb_Orcamento",
                columns: new[] { "TenantId", "ClienteId", "IsActive", "Codigo" },
                descending: new[] { false, false, false, true })
                .Annotation("SqlServer:Include", new[] { "Status", "Validade", "FornecedorId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orcamento_Tenant_Cliente_Codigo",
                table: "tb_Orcamento");

            migrationBuilder.RenameColumn(
                name: "ValorItem",
                table: "tb_ItemOrcamento",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "DescontoItem",
                table: "tb_ItemOrcamento",
                newName: "Desconto");
        }
    }
}
