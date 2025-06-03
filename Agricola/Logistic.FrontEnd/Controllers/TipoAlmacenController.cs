using Microsoft.AspNetCore.Mvc;

namespace Logistic.FrontEnd.Controllers;

public class TipoAlmacenController : Controller
{
    //private readonly IRepository _repository;
    //private readonly IMapper _mapper;

    //public TipoAlmacenController(IRepository repository, IMapper mapper)
    //{
    //    _repository = repository;
    //    _mapper = mapper;
    //}

    public IActionResult Index()
    {
        return View();
    }
}