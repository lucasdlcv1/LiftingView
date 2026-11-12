using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiftingView.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_OrdemAoItemRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "ItensRegistro",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.Sql("""
                WITH ItensOrdenados AS
                (
                    SELECT Id, ROW_NUMBER() OVER (PARTITION BY RegistroId ORDER BY Id) AS NovaOrdem
                    FROM ItensRegistro
                )
                UPDATE item
                SET Ordem = ordenado.NovaOrdem
                FROM ItensRegistro AS item
                INNER JOIN ItensOrdenados AS ordenado ON ordenado.Id = item.Id;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "ItensRegistro");
        }
    }
}
