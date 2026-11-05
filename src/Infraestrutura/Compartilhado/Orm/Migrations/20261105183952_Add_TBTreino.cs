using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiftingView.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBTreino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Treinos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTreino",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExercicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repeticoes = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    IntervaloSegundos = table.Column<int>(type: "int", nullable: false),
                    Carga = table.Column<int>(type: "int", nullable: false),
                    FatorTecnica = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTreino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTreino_TBExercicios_ExercicioId",
                        column: x => x.ExercicioId,
                        principalTable: "TBExercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTreino_Treinos_TreinoId",
                        column: x => x.TreinoId,
                        principalTable: "Treinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemTreino_ExercicioId",
                table: "ItemTreino",
                column: "ExercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTreino_TreinoId",
                table: "ItemTreino",
                column: "TreinoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemTreino");

            migrationBuilder.DropTable(
                name: "Treinos");
        }
    }
}
