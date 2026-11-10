using System.ComponentModel.DataAnnotations;

namespace LiftingView.WebApp.Modulos.Registros;

public sealed class RegistroViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Selecione um treino para usar como modelo.")]
    public Guid? TreinoId { get; set; }

    public string? TreinoNome { get; set; }

    [Required(ErrorMessage = "Informe a data do registro.")]
    [DataType(DataType.Date)]
    public DateTime Data { get; set; } = DateTime.Today;

    [StringLength(1000, ErrorMessage = "As observações não podem exceder 1000 caracteres.")]
    public string Observacoes { get; set; } = string.Empty;

    public List<ItemRegistroViewModel> ItensRegistro { get; set; } = [];
}

public sealed class ItemRegistroViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Selecione um exercício.")]
    public Guid? ExercicioId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Informe pelo menos uma série.")]
    public int SeriesRealizadas { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Informe pelo menos uma repetição.")]
    public int RepeticoesRealizadas { get; set; } = 1;

    [Range(0, int.MaxValue, ErrorMessage = "A carga não pode ser negativa.")]
    public int CargaUtilizada { get; set; }

    [StringLength(1000, ErrorMessage = "As observações não podem exceder 1000 caracteres.")]
    public string Observacoes { get; set; } = string.Empty;
}

public sealed record ExcluirRegistroViewModel(Guid Id, DateTime Data, string? TreinoNome);
