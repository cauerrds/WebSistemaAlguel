using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSistemaAlguel.Data.Migrations
{
    public partial class CriarBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cpf_cliente",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "setor_funcionario",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "fabricantes",
                columns: table => new
                {
                    cod_fabricante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_fabricante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    matriz_fabricante = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabricantes", x => x.cod_fabricante);
                });

            migrationBuilder.CreateTable(
                name: "carro",
                columns: table => new
                {
                    cod_carro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modelo_carro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valor_carro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ano_carro = table.Column<int>(type: "int", nullable: false),
                    alugado_carro = table.Column<bool>(type: "bit", nullable: false),
                    cod_fabricante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carro", x => x.cod_carro);
                    table.ForeignKey(
                        name: "FK_carro_fabricantes_cod_fabricante",
                        column: x => x.cod_fabricante,
                        principalTable: "fabricantes",
                        principalColumn: "cod_fabricante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alguel",
                columns: table => new
                {
                    cod_aluguel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    preco_final_aluguel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_inicio_alguel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_fim_aluguel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ativo_aluguel = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cod_carro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alguel", x => x.cod_aluguel);
                    table.ForeignKey(
                        name: "FK_alguel_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_alguel_carro_cod_carro",
                        column: x => x.cod_carro,
                        principalTable: "carro",
                        principalColumn: "cod_carro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alguel_cod_carro",
                table: "alguel",
                column: "cod_carro");

            migrationBuilder.CreateIndex(
                name: "IX_alguel_Id",
                table: "alguel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_carro_cod_fabricante",
                table: "carro",
                column: "cod_fabricante");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alguel");

            migrationBuilder.DropTable(
                name: "carro");

            migrationBuilder.DropTable(
                name: "fabricantes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "cpf_cliente",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "setor_funcionario",
                table: "AspNetUsers");
        }
    }
}
