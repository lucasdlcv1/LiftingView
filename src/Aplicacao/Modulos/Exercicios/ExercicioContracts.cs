namespace LiftingView.Aplicacao.Modulos.Exercicios;

public record ExercicioDto(Guid Id, string Nome, string Descricao, string GrupoMuscular);

public record CadastrarExercicioDto(string Nome, string Descricao, string GrupoMuscular);

public record EditarExercicioDto(Guid Id, string Nome, string Descricao, string GrupoMuscular);

public record ExcluirExercicioDto(Guid Id, string Nome, string Descricao, string GrupoMuscular);
