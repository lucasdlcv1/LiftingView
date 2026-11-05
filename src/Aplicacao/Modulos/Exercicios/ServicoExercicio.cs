
using LiftingView.Dominio.Modulos.Exercicios;
using FluentResults;
using LiftingView.Dominio.Compartilhado;

namespace LiftingView.Aplicacao.Modulos.Exercicios;

public sealed class ServicoExercicio(IRepositorioExercicio repositorioExercicio)
{
    public Result<Guid> Cadastrar(CadastrarExercicioDto dto)
    {
        Exercicio exercicio = new Exercicio(dto.Nome, dto.Descricao, dto.GrupoMuscular);

        List<string> erros = exercicio.Validar();

        if (repositorioExercicio.ExisteExercicioPorNome(dto.Nome))
        {
            erros.Add("Já existe um exercício com o mesmo nome.");
        }

        if (erros.Count > 0)
        {
            return Result.Fail(erros[0]);
        }

        repositorioExercicio.Cadastrar(exercicio);

        return Result.Ok(exercicio.Id);
    }

    public Result Editar(EditarExercicioDto dto)
    {
        Exercicio exercicioAtualizado = new(dto.Nome, dto.Descricao, dto.GrupoMuscular);

        List<ErroValidacao> erros = exercicioAtualizado.Validar().ToList().Select(mensagem => new ErroValidacao { Campo = "", Mensagem = mensagem }).ToList();

        if (repositorioExercicio.ExisteExercicioPorNome(exercicioAtualizado.Nome, dto.Id))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(exercicioAtualizado.Nome),
                Mensagem = "Já existe uma exercicio cadastrada com o título informado"
            });
        }

        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
                .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        repositorioExercicio.Editar(dto.Id, exercicioAtualizado);

        return Result.Ok();
    }
    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioExercicio.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possível encontrar o exercicio informada.");

        return Result.Ok();
    }
    public Result<ExercicioDto> SelecionarPorId(Guid id)
    {
        Exercicio? exercicio = repositorioExercicio.SelecionarPorId(id);

        if (exercicio == null)
            return Result.Fail("Exercicio não encontrada.");

        return Result.Ok(new ExercicioDto(exercicio.Id, exercicio.Nome, exercicio.Descricao, exercicio.GrupoMuscular));
    }

    public List<ExercicioDto> SelecionarTodos()
    {
        List<Exercicio> exercicios = repositorioExercicio.SelecionarTodos();

        return exercicios.Select(e => new ExercicioDto(e.Id, e.Nome, e.Descricao, e.GrupoMuscular)).ToList();
    }
}
