using System.ComponentModel.DataAnnotations;

namespace LiftingView.Infraestrutura.Compartilhado;

public record ListarExerciciosViewModel
{
    Guid Id { get; set; }
    string Nome { get; set; } = string.Empty;
    string Descricao { get; set; } = string.Empty;
    string GrupoMuscular { get; set; } = string.Empty;
}

public record CadastrarExercicioViewModel
([Required(ErrorMessage = "O nome do exercício é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do exercício não pode exceder 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "A descrição do exercício é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição do exercício não pode exceder 500 caracteres.")]
    string Descricao,

    string GrupoMuscular

);

public record EditarExercicioViewModel(
    Guid Id,
    [Required(ErrorMessage = "O nome do exercício é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do exercício não pode exceder 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "A descrição do exercício é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição do exercício não pode exceder 500 caracteres.")]
    string Descricao,

    string GrupoMuscular
);

public record ExcluirExercicioViewModel(
    Guid Id,
    string Nome
);
