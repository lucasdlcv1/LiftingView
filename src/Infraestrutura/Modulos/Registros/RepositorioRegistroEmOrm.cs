using LiftingView.Dominio.Modulos.Registros;
using LiftingView.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace LiftingView.Infraestrutura.Modulos.Registros;

public sealed class RepositorioRegistroEmOrm(LiftingViewDbContext dbContext)
    : RepositorioBaseEmOrm<Registro>(dbContext), IRepositorioRegistro
{
    public override Registro? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(registro => registro.TreinoOrigem)
            .Include(registro => registro.ItensRegistro)
                .ThenInclude(item => item.Exercicio)
            .SingleOrDefault(registro => registro.Id == idSelecionado);
    }

    public override List<Registro> SelecionarTodos()
    {
        return registros
            .Include(registro => registro.TreinoOrigem)
            .Include(registro => registro.ItensRegistro)
                .ThenInclude(item => item.Exercicio)
            .ToList();
    }
}
