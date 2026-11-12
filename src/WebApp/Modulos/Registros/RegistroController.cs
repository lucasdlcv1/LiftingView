using LiftingView.Aplicacao.Modulos.Exercicios;
using LiftingView.Aplicacao.Modulos.Registros;
using LiftingView.Aplicacao.Modulos.Treinos;
using Microsoft.AspNetCore.Mvc;

namespace LiftingView.WebApp.Modulos.Registros;

public sealed class RegistroController(
    ServicoRegistro servicoRegistro,
    ServicoTreino servicoTreino,
    ServicoExercicio servicoExercicio) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        return View(servicoRegistro.SelecionarTodos());
    }

    [HttpGet]
    public ActionResult Detalhes(Guid id)
    {
        var resultado = servicoRegistro.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        return View(resultado.Value);
    }

    [HttpGet]
    public ActionResult Cadastrar(Guid? treinoId)
    {
        RegistroViewModel viewModel = new();

        if (treinoId.HasValue)
        {
            var resultado = servicoTreino.SelecionarPorId(treinoId.Value);

            if (resultado.IsFailed)
                ModelState.AddModelError(string.Empty, resultado.Errors[0].Message);
            else
            {
                TreinoDto treino = resultado.Value;
                viewModel.TreinoId = treino.Id;
                viewModel.TreinoNome = treino.Nome;
                viewModel.ItensRegistro = treino.ItensTreino
                    .OrderBy(item => item.Ordem)
                    .Select(item => new ItemRegistroViewModel
                    {
                        Ordem = item.Ordem,
                        ExercicioId = item.ExercicioId,
                        SeriesRealizadas = item.Series,
                        RepeticoesRealizadas = item.Repeticoes,
                        CargaUtilizada = item.Carga,
                        FatorTecnica = item.FatorTecnica
                    })
                    .ToList();
            }
        }

        PrepararView(viewModel);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Cadastrar(RegistroViewModel viewModel)
    {
        if (viewModel.ItensRegistro.Count == 0)
            ModelState.AddModelError(nameof(viewModel.ItensRegistro), "Adicione pelo menos um exercício ao registro.");

        if (!ModelState.IsValid)
        {
            PrepararView(viewModel);
            return View(viewModel);
        }

        CadastrarRegistroDto dto = new(
            viewModel.Data,
            viewModel.TreinoId!.Value,
            viewModel.Observacoes ?? string.Empty,
            viewModel.ItensRegistro.Select(item => new CadastrarItemRegistroDto(
                item.ExercicioId!.Value,
                item.Ordem,
                item.SeriesRealizadas,
                item.RepeticoesRealizadas,
                item.CargaUtilizada,
                item.FatorTecnica,
                item.Observacoes ?? string.Empty)).ToList());

        var resultado = servicoRegistro.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(string.Empty, resultado.Errors[0].Message);
            PrepararView(viewModel);
            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        var resultado = servicoRegistro.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        RegistroDto registro = resultado.Value;
        RegistroViewModel viewModel = new()
        {
            Id = registro.Id,
            Data = registro.Data,
            TreinoId = registro.TreinoId,
            TreinoNome = registro.TreinoNome,
            Observacoes = registro.Observacoes,
            ItensRegistro = registro.ItensRegistro
                .Select(item => new ItemRegistroViewModel
                {
                    Id = item.Id,
                    Ordem = item.Ordem,
                    ExercicioId = item.ExercicioId,
                    SeriesRealizadas = item.SeriesRealizadas,
                    RepeticoesRealizadas = item.RepeticoesRealizadas,
                    CargaUtilizada = item.CargaUtilizada,
                    FatorTecnica = item.FatorTecnica,
                    Observacoes = item.Observacoes
                })
                .ToList()
        };

        PrepararView(viewModel);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, RegistroViewModel viewModel)
    {
        if (viewModel.Id != id)
            return BadRequest();

        if (viewModel.ItensRegistro.Count == 0)
            ModelState.AddModelError(nameof(viewModel.ItensRegistro), "Adicione pelo menos um exercício ao registro.");

        if (!ModelState.IsValid)
        {
            PrepararView(viewModel);
            return View(viewModel);
        }

        EditarRegistroDto dto = new(
            id,
            viewModel.Data,
            viewModel.TreinoId,
            viewModel.Observacoes ?? string.Empty,
            viewModel.ItensRegistro.Select(item => new EditarItemRegistroDto(
                item.Id ?? Guid.Empty,
                item.ExercicioId!.Value,
                item.Ordem,
                item.SeriesRealizadas,
                item.RepeticoesRealizadas,
                item.CargaUtilizada,
                item.FatorTecnica,
                item.Observacoes ?? string.Empty)).ToList());

        var resultado = servicoRegistro.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(string.Empty, resultado.Errors[0].Message);
            PrepararView(viewModel);
            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        var resultado = servicoRegistro.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        RegistroDto registro = resultado.Value;
        return View(new ExcluirRegistroViewModel(registro.Id, registro.Data, registro.TreinoNome));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Excluir(ExcluirRegistroViewModel viewModel)
    {
        var resultado = servicoRegistro.Excluir(viewModel.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(string.Empty, resultado.Errors[0].Message);
            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }

    private void PrepararView(RegistroViewModel viewModel)
    {
        ViewBag.Treinos = servicoTreino.SelecionarTodos();
        ViewBag.Exercicios = servicoExercicio.SelecionarTodos();
    }
}
