using AutoMapper;
using Logistic.FrontEnd.Repositories;
using Logistic.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Logistic.FrontEnd.Controllers;

public class GenericTablaGlobalesReportPdfController : Controller
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public GenericTablaGlobalesReportPdfController(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // string titulo = "Tipo Almacén";
    // string fileName = $"Tipo Almacen {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf"
    // string orientation = Portrait     // Landscape,
    // string url = "/api/tipoalmacen";

    public async Task<IActionResult> ListarPDF(string titulo, string fileName, string orientation, string url)
    {
        //var query = new List<TablaGlobalSelectDto>();
        //var (urlBase, token) = await _autenticarService.Autenticar();
        //var httpClient = GenericRequest.RequestApi(urlBase, token.token);

        //using (var response = await httpClient.GetAsync("/api/TipoAlmacen/GetAll"))
        //{
        //    var content = await response.Content.ReadAsStringAsync();
        //    var datos = JsonConvert.DeserializeObject<List<TableGlobal>>(content);
        //    query = _mapper.Map<List<TablasBasicas>>(datos);
        //}

        var query = await _repository.GetAsync<List<TablaGlobalSelectDto>>(url);
        //var query = await _repository.GetAsync<List<TablaGlobalSelectDto>>("/api/tipoalmacen");
        //string titulo = "Tipo Almacén";
        string protocolo = Request.IsHttps ? "Https" : "Http";
        string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
        string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

        //   return new ViewAsPdf("ListarPDF", (List<TablaGlobalSelectDto>)query.Response)
        return new ViewAsPdf("ListarPDF", (List<TablaGlobalSelectDto>)query.Response)
        {
            FileName = fileName, //$"Tipo Almacen {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
            PageOrientation = orientation == "Portrait" ? Rotativa.AspNetCore.Options.Orientation.Portrait : Rotativa.AspNetCore.Options.Orientation.Landscape,
            PageSize = Rotativa.AspNetCore.Options.Size.A4,
            PageMargins = { Left = 5, Bottom = 10, Right = 5, Top = 30 },
            CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
        };
    }
}