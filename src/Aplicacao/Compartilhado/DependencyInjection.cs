
using LiftingView.Aplicacao.Modulos.Exercicios;
using LiftingView.Aplicacao.Modulos.Treinos;
using Microsoft.Extensions.DependencyInjection;

public static class InjecaoDependencia
{
    public static void AdicionarCamadaAplicacao(this IServiceCollection services)
    {
        services.AddScoped<ServicoExercicio>();
        services.AddScoped<ServicoTreino>();
    }
}
