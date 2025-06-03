using Logistic.FrontEnd.Models;
using Logistic.FrontEnd.Repositories;
using Logistic.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Logistic.FrontEnd.Controllers;

public class GenericController : Controller
{
    #region Constructor

    private readonly IRepository _repository;

    public GenericController(IRepository repository)
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

    #region HttpPost => Crear

    [HttpPost]
    public async Task<IActionResult> Crear([FromForm] string url, [FromForm] string model)
    {
        var gResponse = new GenericResponse<object>() { Estado = false };

        try
        {
            var urlApi = JsonConvert.DeserializeObject<string>(url);
            var modelo = JsonConvert.DeserializeObject<TablaGlobalSelectDto>(model);
            var query = await _repository.PostAsync(urlApi, modelo);
            gResponse.Objeto = query.Response;
        }
        catch (Exception ex)
        {
            gResponse.Estado = true;
            gResponse.Mensaje = ex.Message;
        }

        return StatusCode(StatusCodes.Status200OK, gResponse);
    }

    #endregion HttpPost => Crear

    #region HttpPost => Editar

    [HttpPut]
    public async Task<IActionResult> Editar([FromForm] string url, [FromForm] string model)
    {
        var gResponse = new GenericResponse<object>() { Estado = false };

        try
        {
            var urlApi = JsonConvert.DeserializeObject<string>(url);
            var modelo = JsonConvert.DeserializeObject<TablaGlobalSelectDto>(model);
            var query = await _repository.PutAsync(urlApi, modelo);
            gResponse.Objeto = query.Response;
        }
        catch (Exception ex)
        {
            gResponse.Estado = true;
            gResponse.Mensaje = ex.Message;
        }

        return StatusCode(StatusCodes.Status200OK, gResponse);
    }

    #endregion HttpPost => Editar

    #region HttpDelete => Eliminar

    [HttpDelete]
    public async Task<IActionResult> Eliminar(string url)
    {
        var gResponse = new GenericResponse<string>();

        try
        {
            var query = await _repository.DeleteAsync(url);
            gResponse.Estado = query.HttpResponseMessage.StatusCode == HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
            gResponse.Estado = false;
            gResponse.Mensaje = ex.Message;
        }

        return StatusCode(StatusCodes.Status200OK, gResponse);
    }

    #endregion HttpDelete => Eliminar

    #region HttpGet => All

    [HttpGet]
    public async Task<IActionResult> ListaAll(string url)
    {
        var query = await _repository.GetAsync<List<TablaGlobalSelectDto>>(url);

        if (query.Error)
            return StatusCode(StatusCodes.Status200OK, new { data = new List<TablaGlobalSelectDto>() });

        return StatusCode(StatusCodes.Status200OK, new { data = query.Response });
    }

    #endregion HttpGet => All
}