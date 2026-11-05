using LiftingView.Dominio.Modulos.Exercicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiftingView.Infraestrutura.Modulos.Exercicios;

public sealed class ExercicioConfiguration : IEntityTypeConfiguration<Exercicio>
{
    public void Configure(EntityTypeBuilder<Exercicio> builder)
    {
        builder.ToTable("TBExercicios");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(e => e.Nome)
            .IsUnique();

        builder.Property(e => e.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.GrupoMuscular)
            .HasMaxLength(100)
            .IsRequired();
    }
}
