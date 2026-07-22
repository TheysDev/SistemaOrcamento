using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrcamentoSaaS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaDeCodigos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControleDeCodigos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoOrcamento = table.Column<int>(type: "int", nullable: true),
                    CodigoPedido = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleDeCodigos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleDeCodigos");
        }
    }
}
