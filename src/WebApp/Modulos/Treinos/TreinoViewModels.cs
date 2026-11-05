using System.ComponentModel.DataAnnotations;

namespace LiftingView.WebApp.Modulos.Treinos;

public sealed class CadastrarTreinoViewModel
{
    [Required(ErrorMessage = "O nome do treino é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do treino não pode exceder 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    public List<CadastrarItemTreinoViewModel> ItensTreino { get; set; } = [];
}

public sealed class CadastrarItemTreinoViewModel
{
    [Required(ErrorMessage = "Selecione um exercício.")]
    public Guid? ExercicioId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Informe pelo menos uma série.")]
    public int Series { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Informe pelo menos uma repetição.")]
    public int Repeticoes { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "A ordem deve ser maior que zero.")]
    public int Ordem { get; set; } = 1;

    [Range(0, int.MaxValue, ErrorMessage = "O intervalo não pode ser negativo.")]
    public int IntervaloSegundos { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A carga não pode ser negativa.")]
    public int Carga { get; set; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335",
        ErrorMessage = "O fator da técnica não pode ser negativo.")]
    public decimal FatorTecnica { get; set; } = 1;
}

public sealed class EditarTreinoViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome do treino é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do treino não pode exceder 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    public List<EditarItemTreinoViewModel> ItensTreino { get; set; } = [];
}

public sealed class EditarItemTreinoViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Selecione um exercício.")]
    public Guid? ExercicioId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Informe pelo menos uma série.")]
    public int Series { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Informe pelo menos uma repetição.")]
    public int Repeticoes { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "A ordem deve ser maior que zero.")]
    public int Ordem { get; set; } = 1;

    [Range(0, int.MaxValue, ErrorMessage = "O intervalo não pode ser negativo.")]
    public int IntervaloSegundos { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A carga não pode ser negativa.")]
    public int Carga { get; set; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335",
        ErrorMessage = "O fator da técnica não pode ser negativo.")]
    public decimal FatorTecnica { get; set; } = 1;
}
public record ExcluirTreinoViewModel(Guid Id, string Nome);
