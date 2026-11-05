
using FluentResults;
using LiftingView.Aplicacao.Modulos.Exercicios;
using LiftingView.Dominio.Modulos.Exercicios;
using Microsoft.AspNetCore.Mvc;

namespace LiftingView.Infraestrutura.Compartilhado;

public sealed class ExercicioController(ServicoExercicio servicoExercicio) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ExercicioDto> exercicios = servicoExercicio.SelecionarTodos();

        return View(exercicios);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarExercicioViewModel viewModel = new("", "", "");

        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarExercicioViewModel viewModel)
    {
        Result<Guid> resultado = servicoExercicio.Cadastrar(new CadastrarExercicioDto(viewModel.Nome, viewModel.Descricao, viewModel.GrupoMuscular));

        if (resultado.IsFailed)
        {
            string mensagemErro = resultado.Errors[0].Message;

            ModelState.AddModelError(string.Empty, resultado.Errors[0].Message);
            return View(viewModel);
        }
        return RedirectToAction("Listar");
    }


    [HttpGet]

    public ActionResult Editar(Guid id)
    {
        Result<ExercicioDto> resultado = servicoExercicio.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        ExercicioDto dto = resultado.Value;

        EditarExercicioViewModel viewModel = new(
            dto.Id,
            dto.Nome,
            dto.Descricao,
            dto.GrupoMuscular
        );


        return View();
    }

    [HttpPost]

    public ActionResult Editar(Guid id, EditarExercicioViewModel viewModel)
    {
        EditarExercicioDto dto = new(
           viewModel.Id,
           viewModel.Nome ?? string.Empty,
           viewModel.Descricao ?? string.Empty,
           viewModel.GrupoMuscular ?? string.Empty
       );

        Result resultado = servicoExercicio.Editar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]

    public ActionResult Excluir(Guid id)
    {
        Result<ExercicioDto> resultado = servicoExercicio.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        ExercicioDto dto = resultado.Value;

        ExcluirExercicioViewModel viewModel = new(dto.Id, dto.Nome);

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(Guid id, ExcluirExercicioViewModel viewModel)
    {
        servicoExercicio.Excluir(viewModel.Id);

        return RedirectToAction(nameof(Listar));
    }

}
