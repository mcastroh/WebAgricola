using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Logistic.FrontEnd.Controllers;

public abstract class GenericController<Model, Entity> : Controller where Entity : class where Model : Models.Model, new()
{
    #region Constructor

    //private readonly IAutenticarService _autenticarService;
    private readonly IMapper _mapper;

    protected GenericController(
        //IAutenticarService autenticarService, 
        IMapper mapper)
    {
        //_autenticarService = autenticarService;
        _mapper = mapper;
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
    public async Task<IActionResult> Save(Model model)
    {
        var gResponse = new GenericResponse<Entity>() { Estado = false };

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            //var genericModel = new Model();
            var newModel = _mapper.Map<Model>(model);
            var (urlBase, token) = await _autenticarService.Autenticar();
            var httpClient = GenericRequest.RequestApi(urlBase, token.token);

            var (valIsExists, valMesage) = await ServicioGlobalTablas.ValidateCodigoSiExiste(httpClient, $"{model.NameApi}{0}/{newModel.Codigo}", $"Código '{newModel.Codigo}' ya fue registrado.");

            if (valIsExists)
            {
                gResponse.Mensaje = valMesage;
                return StatusCode(StatusCodes.Status200OK, gResponse);
            }

            var content = new StringContent(JsonConvert.SerializeObject(newModel), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(model.NameApi, content);

            if (response.IsSuccessStatusCode)
            {
                gResponse.Estado = true;
                gResponse.Objeto = JsonConvert.DeserializeObject<Entity>(await response.Content.ReadAsStringAsync());
            }
        }
        catch (Exception ex)
        {
            gResponse.Mensaje = ex.Message;
        }

        return StatusCode(StatusCodes.Status200OK, gResponse);
    }

    #endregion HttpPost => Crear

    #region HttpGet => All      string nameApi

    [HttpGet]
    public async Task<IActionResult> ListaAll()
    {
        //var api = new Model();

        var datos = new List<Entity>();
        var (urlBase, token) = await _autenticarService.Autenticar();
        var httpClient = GenericRequest.RequestApi(urlBase, token.token);

        using (var response = await httpClient.GetAsync("/api/tipoAlmacen/getAll"))
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                datos = JsonConvert.DeserializeObject<List<Entity>>(content);
                if (datos == null) datos = new List<Entity>();
            }
        }

        return StatusCode(StatusCodes.Status200OK, new { data = datos });
    }


        #endregion HttpGet => All      string nameApi