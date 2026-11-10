using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiftingView.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Registros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreinoOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registros_Treinos_TreinoOrigemId",
                        column: x => x.TreinoOrigemId,
                        principalTable: "Treinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItensRegistro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExercicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesRealizadas = table.Column<int>(type: "int", nullable: false),
                    RepeticoesRealizadas = table.Column<int>(type: "int", nullable: false),
                    CargaUtilizada = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensRegistro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensRegistro_Registros_RegistroId",
                        column: x => x.RegistroId,
                        principalTable: "Registros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensRegistro_TBExercicios_ExercicioId",
                        column: x => x.ExercicioId,
                        principalTable: "TBExercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensRegistro_ExercicioId",
                table: "ItensRegistro",
                column: "ExercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensRegistro_RegistroId",
                table: "ItensRegistro",
                column: "RegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_TreinoOrigemId",
                table: "Registros",
                column: "TreinoOrigemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensRegistro");

            migrationBuilder.DropTable(
                name: "Registros");
        }
    }
}
