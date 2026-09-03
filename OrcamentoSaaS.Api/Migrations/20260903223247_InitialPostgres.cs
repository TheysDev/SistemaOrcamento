using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OrcamentoSaaS.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    Documento = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_ControleCodigos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenanteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoOrcamento = table.Column<int>(type: "integer", nullable: true),
                    CodigoPedido = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ControleCodigos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Documento = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    PorcentagemAVista = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    PorcentagemAPrazo = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Detalhes = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Orcamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<int>(type: "integer", nullable: false),
                    Validade = table.Column<DateOnly>(type: "date", nullable: false),
                    NumeroParcelas = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Orcamento", x => x.Id);
                    table.CheckConstraint("CK_Orcamento_NumeroParcelas", "[NumeroParcelas] BETWEEN 1 AND 36");
                    table.ForeignKey(
                        name: "FK_tb_Orcamento_tb_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "tb_Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_Orcamento_tb_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "tb_Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_ItemOrcamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrcamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    ValorItem = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DescontoItem = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ItemOrcamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_ItemOrcamento_tb_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "tb_Orcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_ItemOrcamento_tb_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tb_Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Codigo = table.Column<int>(type: "integer", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OrcamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_Pedidos_tb_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "tb_Orcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Parcelas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PedidoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataPagamento = table.Column<DateOnly>(type: "date", nullable: true),
                    Vencimento = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Parcelas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_Parcelas_tb_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "tb_Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Cliente_TenantId_Documento",
                table: "tb_Cliente",
                columns: new[] { "TenantId", "Documento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_ControleCodigos_TenanteId",
                table: "tb_ControleCodigos",
                column: "TenanteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Fornecedor_TenantId_Documento",
                table: "tb_Fornecedor",
                columns: new[] { "TenantId", "Documento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_ItemOrcamento_OrcamentoId",
                table: "tb_ItemOrcamento",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ItemOrcamento_ProdutoId",
                table: "tb_ItemOrcamento",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_Tenant_Cliente_Codigo",
                table: "tb_Orcamento",
                columns: new[] { "TenantId", "ClienteId", "IsActive", "Codigo" },
                descending: new[] { false, false, false, true })
                .Annotation("Npgsql:IndexInclude", new[] { "Status", "Validade", "FornecedorId" });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Orcamento_ClienteId",
                table: "tb_Orcamento",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Orcamento_FornecedorId",
                table: "tb_Orcamento",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Orcamento_TenantId_Codigo",
                table: "tb_Orcamento",
                columns: new[] { "TenantId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Parcelas_PedidoId",
                table: "tb_Parcelas",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Tenant_Orcamento_Codigo",
                table: "tb_Pedidos",
                columns: new[] { "TenantId", "OrcamentoId", "IsActive", "Codigo" },
                descending: new[] { false, false, false, true })
                .Annotation("Npgsql:IndexInclude", new[] { "Data", "Status", "ValorTotal" });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Pedidos_OrcamentoId",
                table: "tb_Pedidos",
                column: "OrcamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Pedidos_TenantId_Codigo",
                table: "tb_Pedidos",
                columns: new[] { "TenantId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Produtos_TenantId_Codigo",
                table: "tb_Produtos",
                columns: new[] { "TenantId", "Codigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tb_ControleCodigos");

            migrationBuilder.DropTable(
                name: "tb_ItemOrcamento");

            migrationBuilder.DropTable(
                name: "tb_Parcelas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tb_Produtos");

            migrationBuilder.DropTable(
                name: "tb_Pedidos");

            migrationBuilder.DropTable(
                name: "tb_Orcamento");

            migrationBuilder.DropTable(
                name: "tb_Cliente");

            migrationBuilder.DropTable(
                name: "tb_Fornecedor");
        }
    }
}
