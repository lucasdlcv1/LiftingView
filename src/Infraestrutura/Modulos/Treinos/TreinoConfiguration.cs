using Microsoft.EntityFrameworkCore;

namespace LiftingView.Dominio.Modulos.Treinos;

public sealed class TreinoConfiguration : IEntityTypeConfiguration<Treino>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Treino> builder)
    {
        builder.ToTable("Treinos");

        builder.HasKey(treino => treino.Id);

        builder.Property(treino => treino.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(treino => treino.ItensTreino)
            .WithOne()
            .HasForeignKey(itemTreino => itemTreino.TreinoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
