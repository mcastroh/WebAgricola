using Logistic.FrontEnd.Repositories;
using Logistic.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.FrontEnd.ViewComponents
{
    public class TipoViaViewComponent : ViewComponent
    {
        private readonly IRepository _repository;

        public TipoViaViewComponent(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = await _repository.GetAsync<List<TablaGlobalSelectDto>>("/api/tipoVia");
            return View(query.Response);
        }
    }
}