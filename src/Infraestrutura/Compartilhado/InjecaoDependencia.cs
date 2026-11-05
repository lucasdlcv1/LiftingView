using LiftingView.Dominio.Modulos.Exercicios;
using LiftingView.Dominio.Modulos.Treinos;
using LiftingView.Infraestrutura.Compartilhado.Orm;
using LiftingView.Infraestrutura.Modulos.Exercicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiftingView.Infraestrutura.Compartilhado;

public static class InjecaoDependencia
{
    public static void AdicionarCamadaInfraestrutura(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LiftingViewDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServer");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("A string de conexão com o banco de dados não foi encontrada.");

            options.UseSqlServer(connectionString, cfg => cfg.EnableRetryOnFailure(3));
        });

        services.AddScoped<IRepositorioExercicio, RepositorioExercicioEmOrm>();
        services.AddScoped<IRepositorioTreino, RepositorioTreinoEmOrm>();
    }
}
