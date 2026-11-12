using LiftingView.Dominio.Compartilhado;
using LiftingView.Dominio.Modulos.Exercicios;

namespace LiftingView.Dominio.Modulos.Treinos;

public sealed class ItemTreino : EntidadeBase<ItemTreino>
{
    public Guid TreinoId { get; set; }

    public Guid ExercicioId { get; set; }

    public int Series { get; set; }

    public int Repeticoes { get; set; }

    public int Ordem { get; set; }

    public int IntervaloSegundos { get; set; }

    public int Carga { get; set; }

    public decimal FatorTecnica { get; set; }

    public Treino Treino { get; set; } = null!;

    public Exercicio Exercicio { get; set; } = null!;

    public decimal CalcularVolumeLoad() =>
        (decimal)Series * Repeticoes * Carga * FatorTecnica;

    public override void Atualizar(ItemTreino entidadeAtualizada)
    {
        ExercicioId = entidadeAtualizada.ExercicioId;
        Series = entidadeAtualizada.Series;
        Repeticoes = entidadeAtualizada.Repeticoes;
        Ordem = entidadeAtualizada.Ordem;
        IntervaloSegundos = entidadeAtualizada.IntervaloSegundos;
        Carga = entidadeAtualizada.Carga;
        FatorTecnica = entidadeAtualizada.FatorTecnica;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (ExercicioId == Guid.Empty)
            erros.Add("O exercício é obrigatório.");

        if (Series <= 0)
            erros.Add("A quantidade de séries deve ser maior que zero.");

        if (Repeticoes <= 0)
            erros.Add("A quantidade de repetições deve ser maior que zero.");

        if (Ordem <= 0)
            erros.Add("A ordem do exercício deve ser maior que zero.");

        if (IntervaloSegundos < 0)
            erros.Add("O intervalo não pode ser negativo.");

        if (Carga < 0)
            erros.Add("A carga não pode ser negativa.");

        if (FatorTecnica < 0)
            erros.Add("O fator da técnica não pode ser negativo.");

        return erros;
    }
}
