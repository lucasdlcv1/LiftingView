using LiftingView.Dominio.Modulos.Exercicios;
using LiftingView.Dominio.Modulos.Treinos;
using LiftingView.Infraestrutura.Modulos.Exercicios;
using Microsoft.EntityFrameworkCore;

namespace LiftingView.Infraestrutura.Compartilhado.Orm;

public sealed class LiftingViewDbContext(DbContextOptions<LiftingViewDbContext> options) : DbContext(options)
{

    public DbSet<Exercicio> Exercicios => Set<Exercicio>();
    public DbSet<Treino> Treinos => Set<Treino>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ExercicioConfiguration());
    }
}
