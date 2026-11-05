using Microsoft.AspNetCore.Mvc;

namespace LiftingView.WebApp.Compartilhado;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
