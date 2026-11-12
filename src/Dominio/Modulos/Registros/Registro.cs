using LiftingView.Dominio.Compartilhado;
using LiftingView.Dominio.Modulos.Treinos;

namespace LiftingView.Dominio.Modulos.Registros;

public sealed class Registro : EntidadeBase<Registro>
{
    public Guid? TreinoOrigemId { get; private set; }
    public Treino? TreinoOrigem { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }

    public List<ItemRegistro> ItensRegistro { get; private set; } = [];
    public string Observacoes { get; private set; } = string.Empty;

    public decimal VolumeLoadTotal =>
        ItensRegistro.Sum(item => item.CalcularVolumeLoad());

    public Registro(Guid? treinoOrigemId, DateTime dataInicio, DateTime? dataFim, string observacoes)
    {
        TreinoOrigemId = treinoOrigemId;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Observacoes = observacoes;
    }

    public void AdicionarItemRegistro(ItemRegistro itemRegistro)
    {
        itemRegistro.VincularAoRegistro(Id);
        ItensRegistro.Add(itemRegistro);
    }

    public override void Atualizar(Registro entidadeAtualizada)
    {
        TreinoOrigemId = entidadeAtualizada.TreinoOrigemId;
        DataInicio = entidadeAtualizada.DataInicio;
        DataFim = entidadeAtualizada.DataFim;
        Observacoes = entidadeAtualizada.Observacoes;

        HashSet<Guid> idsItensAtualizados = entidadeAtualizada.ItensRegistro
            .Select(item => item.Id)
            .ToHashSet();

        ItensRegistro.RemoveAll(item => !idsItensAtualizados.Contains(item.Id));

        foreach (ItemRegistro itemAtualizado in entidadeAtualizada.ItensRegistro)
        {
            ItemRegistro? itemExistente = ItensRegistro
                .SingleOrDefault(item => item.Id == itemAtualizado.Id);

            if (itemExistente is null)
            {
                itemAtualizado.VincularAoRegistro(Id);
                ItensRegistro.Add(itemAtualizado);
                continue;
            }

            itemExistente.Atualizar(itemAtualizado);
        }
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (DataFim.HasValue && DataFim < DataInicio)
            erros.Add("A data de fim não pode ser anterior à data de início.");

        return erros;
    }
}
