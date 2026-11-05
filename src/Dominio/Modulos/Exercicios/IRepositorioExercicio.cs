using LiftingView.Dominio.Compartilhado;

namespace LiftingView.Dominio.Modulos.Exercicios;

public interface IRepositorioExercicio : IRepositorio<Exercicio>
{
    bool ExisteExercicioPorNome(string nome, Guid? idIgnorado = null);

}
