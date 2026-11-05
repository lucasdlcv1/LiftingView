using LiftingView.Dominio.Compartilhado;
using LiftingView.Dominio.Modulos.Exercicios;

namespace LiftingView.Dominio.Modulos.Treinos;

public sealed class Treino : EntidadeBase<Treino>
{
    public string Nome { get; set; } = string.Empty;

    public List<ItemTreino> ItensTreino { get; set; } = [];

    public Treino(string nome)
    {
        Nome = nome;
    }

    public override void Atualizar(Treino entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;

        HashSet<Guid> idsItensAtualizados = entidadeAtualizada.ItensTreino
            .Where(item => item.Id != Guid.Empty)
            .Select(item => item.Id)
            .ToHashSet();

        ItensTreino.RemoveAll(item => !idsItensAtualizados.Contains(item.Id));

        foreach (ItemTreino itemAtualizado in entidadeAtualizada.ItensTreino)
        {
            ItemTreino? itemExistente = itemAtualizado.Id == Guid.Empty
                ? null
                : ItensTreino.SingleOrDefault(item => item.Id == itemAtualizado.Id);

            if (itemExistente is null)
            {
                itemAtualizado.TreinoId = Id;
                ItensTreino.Add(itemAtualizado);
                continue;
            }

            itemExistente.Atualizar(itemAtualizado);
        }
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome do treino é obrigatório.");

        return erros;
    }
}
