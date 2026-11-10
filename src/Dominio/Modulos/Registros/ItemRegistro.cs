using LiftingView.Dominio.Modulos.Exercicios;
using LiftingView.Dominio.Compartilhado;

namespace LiftingView.Dominio.Modulos.Registros;

public sealed class ItemRegistro : EntidadeBase<ItemRegistro>
{
    public Guid RegistroId { get; private set; }
    public Guid ExercicioId { get; private set; }
    public int SeriesRealizadas { get; private set; }
    public int RepeticoesRealizadas { get; private set; }
    public int CargaUtilizada { get; private set; }
    public string Observacoes { get; private set; } = string.Empty;

    public Exercicio Exercicio { get; private set; } = null!;
    public Registro Registro { get; private set; } = null!;

    public ItemRegistro(Guid exercicioId, int seriesRealizadas, int repeticoesRealizadas, int cargaUtilizada, string observacoes)
    {
        ExercicioId = exercicioId;
        SeriesRealizadas = seriesRealizadas;
        RepeticoesRealizadas = repeticoesRealizadas;
        CargaUtilizada = cargaUtilizada;
        Observacoes = observacoes;
    }

    public override void Atualizar(ItemRegistro entidadeAtualizada)
    {
        ExercicioId = entidadeAtualizada.ExercicioId;
        SeriesRealizadas = entidadeAtualizada.SeriesRealizadas;
        RepeticoesRealizadas = entidadeAtualizada.RepeticoesRealizadas;
        CargaUtilizada = entidadeAtualizada.CargaUtilizada;
        Observacoes = entidadeAtualizada.Observacoes;
    }

    internal void VincularAoRegistro(Guid registroId) => RegistroId = registroId;

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (ExercicioId == Guid.Empty)
            erros.Add("O exercício é obrigatório.");

        if (SeriesRealizadas <= 0)
            erros.Add("O número de séries realizadas deve ser maior que zero.");

        if (RepeticoesRealizadas <= 0)
            erros.Add("O número de repetições realizadas deve ser maior que zero.");

        if (CargaUtilizada < 0)
            erros.Add("A carga utilizada não pode ser negativa.");

        return erros;
    }
}
