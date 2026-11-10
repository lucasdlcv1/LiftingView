using LiftingView.Dominio.Modulos.Exercicios;
using LiftingView.Dominio.Modulos.Registros;
using LiftingView.Dominio.Modulos.Treinos;
using LiftingView.Infraestrutura.Modulos.Exercicios;
using LiftingView.Infraestrutura.Modulos.Registros;
using Microsoft.EntityFrameworkCore;

namespace LiftingView.Infraestrutura.Compartilhado.Orm;

public sealed class LiftingViewDbContext(DbContextOptions<LiftingViewDbContext> options) : DbContext(options)
{

    public DbSet<Exercicio> Exercicios => Set<Exercicio>();
    public DbSet<Treino> Treinos => Set<Treino>();
    public DbSet<Registro> Registros => Set<Registro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ExercicioConfiguration());
        modelBuilder.ApplyConfiguration(new TreinoConfiguration());
        modelBuilder.ApplyConfiguration(new RegistroConfiguration());
        modelBuilder.ApplyConfiguration(new ItemRegistroConfiguration());
    }
}
