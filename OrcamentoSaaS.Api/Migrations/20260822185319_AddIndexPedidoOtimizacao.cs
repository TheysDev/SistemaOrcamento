using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrcamentoSaaS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexPedidoOtimizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Tenant_Orcamento_Codigo",
                table: "tb_Pedidos",
                columns: new[] { "TenantId", "OrcamentoId", "IsActive", "Codigo" },
                descending: new[] { false, false, false, true })
                .Annotation("SqlServer:Include", new[] { "Data", "Status", "ValorTotal" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pedidos_Tenant_Orcamento_Codigo",
                table: "tb_Pedidos");
        }
    }
}
