using Microsoft.EntityFrameworkCore;

namespace LiftingView.Dominio.Modulos.Treinos;

public sealed class TreinoConfiguration : IEntityTypeConfiguration<Treino>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Treino> builder)
    {
        builder.ToTable("Treinos");

        builder.HasKey(treino => treino.Id);

        builder.Property(treino => treino.Nome)
            .IsRequired();

        builder.HasMany(treino => treino.ItensTreino)
            .WithOne(itemTreino => itemTreino.Treino)
            .HasForeignKey(itemTreino => itemTreino.TreinoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
