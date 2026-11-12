using LiftingView.Dominio.Modulos.Exercicios;
using LiftingView.Dominio.Compartilhado;

namespace LiftingView.Dominio.Modulos.Registros;

public sealed class ItemRegistro : EntidadeBase<ItemRegistro>
{
    public Guid RegistroId { get; private set; }
    public Guid ExercicioId { get; private set; }
    public int Ordem { get; private set; }
    public int SeriesRealizadas { get; private set; }
    public int RepeticoesRealizadas { get; private set; }
    public int CargaUtilizada { get; private set; }
    public decimal FatorTecnica { get; private set; }
    public string Observacoes { get; private set; } = string.Empty;

    public Exercicio Exercicio { get; private set; } = null!;
    public Registro Registro { get; private set; } = null!;

    public ItemRegistro(Guid exercicioId, int ordem, int seriesRealizadas, int repeticoesRealizadas, int cargaUtilizada, decimal fatorTecnica, string observacoes)
    {
        ExercicioId = exercicioId;
        Ordem = ordem;
        SeriesRealizadas = seriesRealizadas;
        RepeticoesRealizadas = repeticoesRealizadas;
        CargaUtilizada = cargaUtilizada;
        FatorTecnica = fatorTecnica;
        Observacoes = observacoes;
    }

    public decimal CalcularVolumeLoad() =>
        (decimal)SeriesRealizadas * RepeticoesRealizadas * CargaUtilizada * FatorTecnica;

    public override void Atualizar(ItemRegistro entidadeAtualizada)
    {
        ExercicioId = entidadeAtualizada.ExercicioId;
        Ordem = entidadeAtualizada.Ordem;
        SeriesRealizadas = entidadeAtualizada.SeriesRealizadas;
        RepeticoesRealizadas = entidadeAtualizada.RepeticoesRealizadas;
        CargaUtilizada = entidadeAtualizada.CargaUtilizada;
        FatorTecnica = entidadeAtualizada.FatorTecnica;
        Observacoes = entidadeAtualizada.Observacoes;
    }

    internal void VincularAoRegistro(Guid registroId) => RegistroId = registroId;

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (ExercicioId == Guid.Empty)
            erros.Add("O exercício é obrigatório.");

        if (Ordem <= 0)
            erros.Add("A ordem do exercício deve ser maior que zero.");

        if (SeriesRealizadas <= 0)
            erros.Add("O número de séries realizadas deve ser maior que zero.");

        if (RepeticoesRealizadas <= 0)
            erros.Add("O número de repetições realizadas deve ser maior que zero.");

        if (CargaUtilizada < 0)
            erros.Add("A carga utilizada não pode ser negativa.");

        if (FatorTecnica < 0)
            erros.Add("O fator da técnica não pode ser negativo.");

        return erros;
    }
}
