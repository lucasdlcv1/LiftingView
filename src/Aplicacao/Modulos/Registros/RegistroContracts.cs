namespace LiftingView.Aplicacao.Modulos.Registros;

public record CadastrarRegistroDto(
    DateTime Data,
    Guid TreinoId,
    string Observacoes,
    List<CadastrarItemRegistroDto> ItensRegistro);

public record CadastrarItemRegistroDto(
    Guid ExercicioId,
    int Ordem,
    int SeriesRealizadas,
    int RepeticoesRealizadas,
    int CargaUtilizada,
    decimal FatorTecnica,
    string Observacoes);

public record EditarRegistroDto(
    Guid Id,
    DateTime Data,
    Guid? TreinoId,
    string Observacoes,
    List<EditarItemRegistroDto> ItensRegistro);

public record EditarItemRegistroDto(
    Guid Id,
    Guid ExercicioId,
    int Ordem,
    int SeriesRealizadas,
    int RepeticoesRealizadas,
    int CargaUtilizada,
    decimal FatorTecnica,
    string Observacoes);

public record RegistroDto(
    Guid Id,
    DateTime Data,
    Guid? TreinoId,
    string? TreinoNome,
    string Observacoes,
    List<ItemRegistroDto> ItensRegistro,
    decimal VolumeLoadTotal);

public record ItemRegistroDto(
    Guid Id,
    Guid ExercicioId,
    string ExercicioNome,
    int Ordem,
    int SeriesRealizadas,
    int RepeticoesRealizadas,
    int CargaUtilizada,
    decimal FatorTecnica,
    string Observacoes,
    decimal VolumeLoad);
