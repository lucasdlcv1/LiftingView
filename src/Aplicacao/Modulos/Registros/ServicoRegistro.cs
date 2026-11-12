using FluentResults;
using LiftingView.Dominio.Modulos.Exercicios;
using LiftingView.Dominio.Modulos.Registros;
using LiftingView.Dominio.Modulos.Treinos;

namespace LiftingView.Aplicacao.Modulos.Registros;

public sealed class ServicoRegistro(
    IRepositorioRegistro repositorioRegistro,
    IRepositorioTreino repositorio,
    IRepositorioExercicio repositorioExercicio)
{
    public Result<Guid> Cadastrar(CadastrarRegistroDto dto)
    {
        Treino? treino = repositorio.SelecionarPorId(dto.TreinoId);

        if (treino is null)
            return Result.Fail("Treino não encontrado.");

        List<string> erros = [];

        if (dto.ItensRegistro.Count == 0)
            erros.Add("O registro deve possuir pelo menos um exercício.");

        List<ItemRegistro> itensRegistro = [];

        foreach (CadastrarItemRegistroDto itemDto in dto.ItensRegistro)
        {
            ItemRegistro itemRegistro = new(
                itemDto.ExercicioId,
                itemDto.Ordem,
                itemDto.SeriesRealizadas,
                itemDto.RepeticoesRealizadas,
                itemDto.CargaUtilizada,
                itemDto.FatorTecnica,
                itemDto.Observacoes);
            erros.AddRange(itemRegistro.Validar());

            if (repositorioExercicio.SelecionarPorId(itemDto.ExercicioId) is null)
                erros.Add("Um dos exercícios selecionados não foi encontrado.");

            itensRegistro.Add(itemRegistro);
        }

        if (erros.Count > 0)
            return Result.Fail(erros[0]);

        Registro registro = new(
            dto.TreinoId,
            dto.Data.Date,
            null,
            dto.Observacoes);

        foreach (ItemRegistro itemRegistro in itensRegistro)
            registro.AdicionarItemRegistro(itemRegistro);

        repositorioRegistro.Cadastrar(registro);

        return Result.Ok(registro.Id);
    }

    public Result Editar(EditarRegistroDto dto)
    {
        Registro? registroExistente = repositorioRegistro.SelecionarPorId(dto.Id);

        if (registroExistente is null)
            return Result.Fail("Registro não encontrado.");

        if (dto.TreinoId.HasValue && repositorio.SelecionarPorId(dto.TreinoId.Value) is null)
            return Result.Fail("Treino não encontrado.");

        List<string> erros = [];

        if (dto.ItensRegistro.Count == 0)
            erros.Add("O registro deve possuir pelo menos um exercício.");

        HashSet<Guid> idsItensExistentes = registroExistente.ItensRegistro
            .Select(item => item.Id)
            .ToHashSet();
        HashSet<Guid> idsItensRecebidos = [];
        List<ItemRegistro> itensRegistro = [];

        foreach (EditarItemRegistroDto itemDto in dto.ItensRegistro)
        {
            if (itemDto.Id != Guid.Empty)
            {
                if (!idsItensExistentes.Contains(itemDto.Id))
                {
                    erros.Add("Um dos itens informados não pertence a este registro.");
                    continue;
                }

                if (!idsItensRecebidos.Add(itemDto.Id))
                {
                    erros.Add("Um item do registro foi informado mais de uma vez.");
                    continue;
                }
            }

            ItemRegistro itemRegistro = new(
                itemDto.ExercicioId,
                itemDto.Ordem,
                itemDto.SeriesRealizadas,
                itemDto.RepeticoesRealizadas,
                itemDto.CargaUtilizada,
                itemDto.FatorTecnica,
                itemDto.Observacoes)
            {
                Id = itemDto.Id == Guid.Empty ? Guid.CreateVersion7() : itemDto.Id
            };

            erros.AddRange(itemRegistro.Validar());

            if (repositorioExercicio.SelecionarPorId(itemDto.ExercicioId) is null)
                erros.Add("Um dos exercícios selecionados não foi encontrado.");

            itensRegistro.Add(itemRegistro);
        }

        if (erros.Count > 0)
            return Result.Fail(erros[0]);

        Registro registroAtualizado = new(
            dto.TreinoId,
            dto.Data.Date,
            registroExistente.DataFim,
            dto.Observacoes)
        {
            Id = dto.Id
        };

        foreach (ItemRegistro itemRegistro in itensRegistro)
            registroAtualizado.AdicionarItemRegistro(itemRegistro);

        if (!repositorioRegistro.Editar(dto.Id, registroAtualizado))
            return Result.Fail("Não foi possível atualizar o registro.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        if (!repositorioRegistro.Excluir(id))
            return Result.Fail("Não foi possível encontrar o registro informado.");

        return Result.Ok();
    }

    public Result<RegistroDto> SelecionarPorId(Guid id)
    {
        Registro? registro = repositorioRegistro.SelecionarPorId(id);

        if (registro is null)
            return Result.Fail("Registro não encontrado.");

        return Result.Ok(ConverterParaDto(registro));
    }

    public List<RegistroDto> SelecionarTodos()
    {
        return repositorioRegistro.SelecionarTodos()
            .OrderByDescending(registro => registro.DataInicio)
            .Select(ConverterParaDto)
            .ToList();
    }

    private static RegistroDto ConverterParaDto(Registro registro)
    {
        List<ItemRegistroDto> itensRegistro = registro.ItensRegistro
            .OrderBy(item => item.Ordem)
            .Select(item => new ItemRegistroDto(
                item.Id,
                item.ExercicioId,
                item.Exercicio.Nome,
                item.Ordem,
                item.SeriesRealizadas,
                item.RepeticoesRealizadas,
                item.CargaUtilizada,
                item.FatorTecnica,
                item.Observacoes,
                item.CalcularVolumeLoad()))
            .ToList();

        return new RegistroDto(
            registro.Id,
            registro.DataInicio,
            registro.TreinoOrigemId,
            registro.TreinoOrigem?.Nome,
            registro.Observacoes,
            itensRegistro,
            registro.VolumeLoadTotal);
    }
}
