namespace LiftingView.Aplicacao.Modulos.Treinos;

public record CadastrarTreinoDto(string Nome, List<CadastrarItemTreinoDto> ItensTreino);

public record CadastrarItemTreinoDto(
    Guid ExercicioId,
    int Series,
    int Repeticoes,
    int Ordem,
    int IntervaloSegundos,
    int Carga,
    decimal FatorTecnica);

public record EditarTreinoDto(Guid Id, string Nome, List<EditarItemTreinoDto> ItensTreino);

public record EditarItemTreinoDto(
    Guid Id,
    Guid ExercicioId,
    int Series,
    int Repeticoes,
    int Ordem,
    int IntervaloSegundos,
    int Carga,
    decimal FatorTecnica);

public record TreinoDto(Guid Id, string Nome, List<ItemTreinoDto> ItensTreino, decimal VolumeLoadTotal);

public record ItemTreinoDto(Guid Id, Guid TreinoId, Guid ExercicioId, string ExercicioNome, int Series, int Repeticoes, int Ordem, int IntervaloSegundos, int Carga, decimal FatorTecnica, decimal VolumeLoad);
