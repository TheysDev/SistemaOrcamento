using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrcamentoSaaS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantIdControleCodigos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ControleDeCodigos",
                table: "ControleDeCodigos");

            migrationBuilder.RenameTable(
                name: "ControleDeCodigos",
                newName: "tb_ControleCodigos");

            migrationBuilder.AddColumn<Guid>(
                name: "TenanteId",
                table: "tb_ControleCodigos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_ControleCodigos",
                table: "tb_ControleCodigos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ControleCodigos_TenanteId",
                table: "tb_ControleCodigos",
                column: "TenanteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_ControleCodigos",
                table: "tb_ControleCodigos");

            migrationBuilder.DropIndex(
                name: "IX_tb_ControleCodigos_TenanteId",
                table: "tb_ControleCodigos");

            migrationBuilder.DropColumn(
                name: "TenanteId",
                table: "tb_ControleCodigos");

            migrationBuilder.RenameTable(
                name: "tb_ControleCodigos",
                newName: "ControleDeCodigos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControleDeCodigos",
                table: "ControleDeCodigos",
                column: "Id");
        }
    }
}
