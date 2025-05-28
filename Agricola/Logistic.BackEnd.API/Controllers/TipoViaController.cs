using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Logistic.Shared.DTOs;
using Logistic.Shared.Entites;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Logistic.BackEnd.API.Controllers;

//[Authorize]
//[Produces("application/json")]
//[Route("api/[controller]")]
//[ApiController]

[ApiController]
[Route("api/[controller]")]
public class TipoViaController : GenericController<TipoVia>
{
    #region Constructor

    private readonly IGenericUnitOfWork<TipoVia> _unitOfWork;

    public TipoViaController(IGenericUnitOfWork<TipoVia> unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion Constructor

    /// <summary>
    /// Recuperar tipos de vías
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _unitOfWork.GetAsync();
        if (response.IsSuccess) return Ok(response.Result);
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _unitOfWork.GetAsync(id);
        if (response.IsSuccess) return Ok(response.Result);
        return NotFound(response.Message);
    }

    [HttpGet("totalRecordsPaginated")]
    public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _unitOfWork.GetAsync(pagination);
        if (action.IsSuccess) return Ok(action.Result);
        return BadRequest();
    }
}