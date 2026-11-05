using LiftingView.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace LiftingView.Dominio.Modulos.Treinos;

public sealed class RepositorioTreinoEmOrm(LiftingViewDbContext dbContext)
 : RepositorioBaseEmOrm<Treino>(dbContext), IRepositorioTreino
{
    public override Treino? SelecionarPorId(Guid idSelecionado)
    {
        return registros
        .Include(treino => treino.ItensTreino)
        .SingleOrDefault(treino => treino.Id == idSelecionado);
    }

    public override List<Treino> SelecionarTodos()
    {
        return registros
            .Include(treino => treino.ItensTreino)
            .ToList();
    }

}
