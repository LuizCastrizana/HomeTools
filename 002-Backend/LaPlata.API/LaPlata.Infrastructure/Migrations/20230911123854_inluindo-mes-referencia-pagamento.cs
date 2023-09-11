using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaPlata.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inluindomesreferenciapagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnoReferencia",
                table: "PagamentosContaVariavel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MesReferencia",
                table: "PagamentosContaVariavel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnoReferencia",
                table: "PagamentosConta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MesReferencia",
                table: "PagamentosConta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoReferencia",
                table: "PagamentosContaVariavel");

            migrationBuilder.DropColumn(
                name: "MesReferencia",
                table: "PagamentosContaVariavel");

            migrationBuilder.DropColumn(
                name: "AnoReferencia",
                table: "PagamentosConta");

            migrationBuilder.DropColumn(
                name: "MesReferencia",
                table: "PagamentosConta");
        }
    }
}
