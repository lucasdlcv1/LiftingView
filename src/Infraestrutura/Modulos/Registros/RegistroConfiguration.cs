using LiftingView.Dominio.Modulos.Registros;
using Microsoft.EntityFrameworkCore;

namespace LiftingView.Infraestrutura.Modulos.Registros;

public sealed class RegistroConfiguration : IEntityTypeConfiguration<Registro>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Registro> builder)
    {
        builder.ToTable("Registros");

        builder.HasKey(registro => registro.Id);

        builder.Property(registro => registro.Observacoes)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(registro => registro.TreinoOrigem)
            .WithMany()
            .HasForeignKey(registro => registro.TreinoOrigemId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(registro => registro.ItensRegistro)
            .WithOne(item => item.Registro)
            .HasForeignKey(item => item.RegistroId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
