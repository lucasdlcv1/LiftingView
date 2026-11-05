using Microsoft.AspNetCore.Mvc;
using LiftingView.Aplicacao.Modulos.Treinos;
using LiftingView.Aplicacao.Modulos.Exercicios;

namespace LiftingView.WebApp.Modulos.Treinos;

public sealed class TreinoController(
    ServicoTreino servicoTreino,
    ServicoExercicio servicoExercicio) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        return View(servicoTreino.SelecionarTodos());
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTreinoViewModel viewModel = new();
        viewModel.ItensTreino.Add(new CadastrarItemTreinoViewModel { Ordem = 1 });
        return PrepararViewCadastro(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarTreinoViewModel viewModel)
    {
        if (viewModel.ItensTreino.Count == 0)
            ModelState.AddModelError(nameof(viewModel.ItensTreino), "Adicione pelo menos um exercício ao treino.");

        if (!ModelState.IsValid)
            return PrepararViewCadastro(viewModel);

        CadastrarTreinoDto dto = new(
            viewModel.Nome,
            viewModel.ItensTreino.Select(item => new CadastrarItemTreinoDto(
                item.ExercicioId!.Value,
                item.Series,
                item.Repeticoes,
                item.Ordem,
                item.IntervaloSegundos,
                item.Carga,
                item.FatorTecnica)).ToList());

        var resultado = servicoTreino.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(string.Empty, resultado.Errors[0].Message);
            return PrepararViewCadastro(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        var resultado = servicoTreino.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        TreinoDto treino = resultado.Value;
        EditarTreinoViewModel viewModel = new()
        {
            Id = treino.Id,
            Nome = treino.Nome,
            ItensTreino = treino.ItensTreino
                .OrderBy(item => item.Ordem)
                .Select(item => new EditarItemTreinoViewModel
                {
                    Id = item.Id,
                    ExercicioId = item.ExercicioId,
                    Series = item.Series,
                    Repeticoes = item.Repeticoes,
                    Ordem = item.Ordem,
                    IntervaloSegundos = item.IntervaloSegundos,
                    Carga = item.Carga,
                    FatorTecnica = item.FatorTecnica
                })
                .ToList()
        };

        return PrepararViewEdicao(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(Guid id, EditarTreinoViewModel viewModel)
    {
        if (id != viewModel.Id)
            return BadRequest();

        if (viewModel.ItensTreino.Count == 0)
            ModelState.AddModelError(nameof(viewModel.ItensTreino), "Adicione pelo menos um exercício ao treino.");

        if (!ModelState.IsValid)
            return PrepararViewEdicao(viewModel);

        EditarTreinoDto dto = new(
            viewModel.Id,
            viewModel.Nome,
            viewModel.ItensTreino.Select(item => new EditarItemTreinoDto(
                item.Id ?? Guid.Empty,
                item.ExercicioId!.Value,
                item.Series,
                item.Repeticoes,
                item.Ordem,
                item.IntervaloSegundos,
                item.Carga,
                item.FatorTecnica)).ToList());

        var resultado = servicoTreino.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(string.Empty, resultado.Errors[0].Message);
            return PrepararViewEdicao(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }

    private ActionResult PrepararViewCadastro(CadastrarTreinoViewModel viewModel)
    {
        ViewBag.Exercicios = servicoExercicio.SelecionarTodos();
        return View("Cadastrar", viewModel);
    }

    private ActionResult PrepararViewEdicao(EditarTreinoViewModel viewModel)
    {
        ViewBag.Exercicios = servicoExercicio.SelecionarTodos();
        return View("Editar", viewModel);
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        var resultado = servicoTreino.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        TreinoDto treino = resultado.Value;
        return View(new ExcluirTreinoViewModel(treino.Id, treino.Nome));
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirTreinoViewModel viewModel)
    {
        var resultado = servicoTreino.Excluir(viewModel.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(string.Empty, resultado.Errors[0].Message);
            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }
}
