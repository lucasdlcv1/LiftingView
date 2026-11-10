using LiftingView.Dominio.Modulos.Registros;
using Microsoft.EntityFrameworkCore;

namespace LiftingView.Infraestrutura.Modulos.Registros;

public sealed class ItemRegistroConfiguration : IEntityTypeConfiguration<ItemRegistro>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ItemRegistro> builder)
    {
        builder.ToTable("ItensRegistro");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Observacoes)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(item => item.Exercicio)
            .WithMany()
            .HasForeignKey(item => item.ExercicioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
