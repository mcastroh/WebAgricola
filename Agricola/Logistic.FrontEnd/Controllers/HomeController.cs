using Logistic.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Logistic.FrontEnd.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #region Rotativa => Cabecera

    public IActionResult Header(string titulo, string logo)
    {
        ViewBag.Titulo = titulo;
        ViewBag.Logo = logo;
        return View("_Header");
    }

    #endregion Rotativa => Cabecera

    #region Rotativa => Pie de Página

    public IActionResult Footer(int page)
    {
        ViewBag.Pagina = page;
        return View("_Footer");
    }

    #endregion Rotativa => Pie de Página
}