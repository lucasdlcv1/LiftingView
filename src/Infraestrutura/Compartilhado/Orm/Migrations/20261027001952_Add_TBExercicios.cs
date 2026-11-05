using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiftingView.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBExercicios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBExercicios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GrupoMuscular = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBExercicios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBExercicios_Nome",
                table: "TBExercicios",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBExercicios");
        }
    }
}
