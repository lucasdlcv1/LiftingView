using FluentResults;
using LiftingView.Dominio.Modulos.Exercicios;
using LiftingView.Dominio.Modulos.Treinos;

namespace LiftingView.Aplicacao.Modulos.Treinos;

public sealed class ServicoTreino(
    IRepositorioTreino repositorioTreino,
    IRepositorioExercicio repositorioExercicio)
{
    public Result<Guid> Cadastrar(CadastrarTreinoDto dto)
    {
        Treino treino = new(dto.Nome);
        List<string> erros = treino.Validar();

        if (dto.ItensTreino.Count == 0)
            erros.Add("O treino deve possuir pelo menos um exercício.");

        foreach (CadastrarItemTreinoDto itemDto in dto.ItensTreino)
        {
            ItemTreino item = new()
            {
                TreinoId = treino.Id,
                ExercicioId = itemDto.ExercicioId,
                Series = itemDto.Series,
                Repeticoes = itemDto.Repeticoes,
                Ordem = itemDto.Ordem,
                IntervaloSegundos = itemDto.IntervaloSegundos,
                Carga = itemDto.Carga,
                FatorTecnica = itemDto.FatorTecnica
            };

            erros.AddRange(item.Validar());

            if (repositorioExercicio.SelecionarPorId(item.ExercicioId) is null)
                erros.Add("Um dos exercícios selecionados não foi encontrado.");

            treino.ItensTreino.Add(item);
        }

        if (erros.Count > 0)
            return Result.Fail(erros[0]);

        repositorioTreino.Cadastrar(treino);

        return Result.Ok(treino.Id);
    }

    public Result Editar(EditarTreinoDto dto)
    {
        Treino? treinoExistente = repositorioTreino.SelecionarPorId(dto.Id);

        if (treinoExistente is null)
            return Result.Fail("Treino não encontrado.");

        Treino treinoAtualizado = new(dto.Nome)
        {
            Id = dto.Id
        };

        List<string> erros = treinoAtualizado.Validar();

        if (dto.ItensTreino.Count == 0)
            erros.Add("O treino deve possuir pelo menos um exercício.");

        HashSet<Guid> idsItensExistentes = treinoExistente.ItensTreino
            .Select(item => item.Id)
            .ToHashSet();
        HashSet<Guid> idsItensRecebidos = [];

        foreach (EditarItemTreinoDto itemDto in dto.ItensTreino)
        {
            if (itemDto.Id != Guid.Empty)
            {
                if (!idsItensExistentes.Contains(itemDto.Id))
                {
                    erros.Add("Um dos itens informados não pertence a este treino.");
                    continue;
                }

                if (!idsItensRecebidos.Add(itemDto.Id))
                {
                    erros.Add("Um item do treino foi informado mais de uma vez.");
                    continue;
                }
            }

            ItemTreino item = new()
            {
                Id = itemDto.Id == Guid.Empty ? Guid.CreateVersion7() : itemDto.Id,
                TreinoId = dto.Id,
                ExercicioId = itemDto.ExercicioId,
                Series = itemDto.Series,
                Repeticoes = itemDto.Repeticoes,
                Ordem = itemDto.Ordem,
                IntervaloSegundos = itemDto.IntervaloSegundos,
                Carga = itemDto.Carga,
                FatorTecnica = itemDto.FatorTecnica
            };

            erros.AddRange(item.Validar());

            if (repositorioExercicio.SelecionarPorId(item.ExercicioId) is null)
                erros.Add("Um dos exercícios selecionados não foi encontrado.");

            treinoAtualizado.ItensTreino.Add(item);
        }

        if (erros.Count > 0)
            return Result.Fail(erros[0]);

        if (!repositorioTreino.Editar(dto.Id, treinoAtualizado))
            return Result.Fail("Não foi possível atualizar o treino.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioTreino.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possível encontrar o treino informado.");

        return Result.Ok();
    }

    public Result<TreinoDto> SelecionarPorId(Guid id)
    {
        Treino? treino = repositorioTreino.SelecionarPorId(id);

        if (treino is null)
            return Result.Fail("Treino não encontrado.");

        return Result.Ok(ConverterParaDto(treino));
    }

    public List<TreinoDto> SelecionarTodos()
    {
        return repositorioTreino.SelecionarTodos()
            .Select(ConverterParaDto)
            .ToList();
    }

    private static TreinoDto ConverterParaDto(Treino treino)
    {
        List<ItemTreinoDto> itensTreino = treino.ItensTreino
            .Select(item => new ItemTreinoDto(
                item.Id,
                item.TreinoId,
                item.ExercicioId,
                item.Exercicio.Nome,
                item.Series,
                item.Repeticoes,
                item.Ordem,
                item.IntervaloSegundos,
                item.Carga,
                item.FatorTecnica,
                item.CalcularVolumeLoad()))
            .ToList();

        return new TreinoDto(treino.Id, treino.Nome, itensTreino, treino.VolumeLoadTotal);
    }
}