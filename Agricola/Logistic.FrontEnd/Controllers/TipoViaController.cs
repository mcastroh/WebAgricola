using Logistic.FrontEnd.Repositories;
using Logistic.Shared.DTOs;
using Logistic.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.FrontEnd.Controllers;

public class TipoViaController : Controller
{
    #region Constructor

    private readonly IRepository _repository;

    public TipoViaController(IRepository repository)
    {
        _repository = repository;
    }

    #endregion Constructor

    #region Index

    public async Task<IActionResult> Index()
    {
        var query = await _repository.GetAsync<List<TablaGlobalSelectDto>>("/api/Tipovia");
        return View();
    }

    #endregion Index

    //public TipoViaController()
    //{ }

    //[HttpGet]
    //public async Task<IActionResult> Index()
    //{
    //    var http = new HttpClient();
    //    http.BaseAddress = new Uri("https://localhost:8084");

    //    using (var response = await http.GetAsync("/api/tipoVia"))
    //    {
    //        if (response.IsSuccessStatusCode)
    //        {
    //            var content = await response.Content.ReadAsStringAsync();
    //        }
    //    }

    //    return StatusCode(StatusCodes.Status200OK);
}