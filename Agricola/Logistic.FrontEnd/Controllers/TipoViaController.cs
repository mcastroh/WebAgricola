using Logistic.FrontEnd.Repositories;
using Logistic.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public IActionResult Index()
    {
        return View();
    }

    #endregion Index

    [HttpGet]
    public async Task<IActionResult> ListaAll()
    {
        var query = await _repository.GetAsync<List<TablaGlobalSelectDto>>("/api/tipoVia");
        return StatusCode(StatusCodes.Status200OK, new { data = query.Response });
    }

    //[HttpGet]
    //public async Task<IActionResult> ListaAll()
    //{
    //    var datos = new List<TableGlobal>();
    //    var (urlBase, token) = await _autenticarService.Autenticar();
    //    var httpClient = GenericRequest.RequestApi(urlBase, token.token);

    //    using (var response = await httpClient.GetAsync("/api/tipoVia"))
    //    {
    //        if (response.IsSuccessStatusCode)
    //        {
    //            var content = await response.Content.ReadAsStringAsync();
    //            datos = JsonConvert.DeserializeObject<List<TableGlobal>>(content);
    //            if (datos == null) datos = new List<TableGlobal>();
    //        }
    //    }

    //    return StatusCode(StatusCodes.Status200OK, new { data = datos });
    //}

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