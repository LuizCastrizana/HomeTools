using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaPlata.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class campopaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paga",
                table: "Compras",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paga",
                table: "Compras");
        }
    }
}
