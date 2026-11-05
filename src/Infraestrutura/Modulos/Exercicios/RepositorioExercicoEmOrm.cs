
using LiftingView.Dominio.Modulos.Exercicios;
using LiftingView.Infraestrutura.Compartilhado.Orm;

namespace LiftingView.Infraestrutura.Modulos.Exercicios;

public sealed class RepositorioExercicioEmOrm(LiftingViewDbContext dbContext) : RepositorioBaseEmOrm<Exercicio>(dbContext), IRepositorioExercicio
{
    public List<Exercicio> SelecionarTodos()
    {
        return dbContext.Exercicios
            .OrderBy(e => e.Nome)
            .ToList();
    }

    public bool ExisteExercicioPorNome(string nomeExercicio, Guid? idIgnorado = null)
    {
        return dbContext.Exercicios.Any(e =>
            e.Id != idIgnorado &&
            e.Nome.ToLower() == nomeExercicio.ToLower()
        );
    }
}
