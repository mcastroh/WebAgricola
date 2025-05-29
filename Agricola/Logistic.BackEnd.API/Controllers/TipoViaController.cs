using AutoMapper;
using Logistic.BackEnd.API.Mappings;
using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Logistic.Shared.DTOs;
using Logistic.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.BackEnd.API.Controllers;

//[Authorize]
//[Produces("application/json")]

[ApiController]
[Route("api/[controller]")]
public class TipoViaController : GenericController<TipoVia>
{
    #region Constructor

    private readonly IGenericUnitOfWork<TipoVia> _unitOfWork;
    private readonly IMapper _mapper;

    public TipoViaController(IGenericUnitOfWork<TipoVia> unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #endregion Constructor

    #region HttpGet => Recuperar tipos de vías

    /// <summary>
    /// Recuperar tipos de vías
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _unitOfWork.GetAsync();
        if (response.IsSuccess) return Ok(AutomapperGeneric<TipoVia, TablaGlobalSelectDto>.MapList(response.Result.ToList()));
        return BadRequest();
    }

    #endregion HttpGet => Recuperar tipos de vías

    #region HttpGet => Recuperar tipo de vía por ID

    /// <summary>
    /// Recuperar tipo de vía por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _unitOfWork.GetAsync(id);
        if (response.IsSuccess) return Ok(AutomapperGeneric<TipoVia, TablaGlobalSelectDto>.Map(response.Result));
        return NotFound(response.Message);
    }

    #endregion HttpGet => Recuperar tipo de vía por ID

    #region HttpGet => Total registros

    /// <summary>
    /// Total registros tipos de vías
    /// </summary>
    /// <returns></returns>
    [HttpGet("totalRecordsPaginated")]
    public override async Task<IActionResult> GetTotalRecordsAsync()
    {
        var action = await _unitOfWork.GetTotalRecordsAsync();
        if (action.IsSuccess) return Ok(action.Result);
        return BadRequest();
    }

    #endregion HttpGet => Total registros

    #region HttpGet => Recuperar tipo de vía por página

    /// <summary>
    /// Recuperar tipo de vía por página
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        //pagination.Email = User.Identity!.Name;
        var response = await _unitOfWork.GetAsync(pagination);
        if (response.IsSuccess) return Ok(response.Result);
        return BadRequest();
    }

    #endregion HttpGet => Recuperar tipo de vía por página

    #region HttpPost => Adiciona tipo de vía

    /// <summary>
    /// Adicionar tipo de vía
    /// </summary>
    /// <param name="entityDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync(TablaGlobalDto entityDto)
    {
        var newEntity = AutomapperGeneric<TablaGlobalDto, TipoVia>.Map(entityDto);
        newEntity.AuditInsertFecha = DateTime.Now;
        newEntity.AuditInsertUsuario = entityDto.AuditUsuario;

        var action = await _unitOfWork.AddAsync(newEntity);
        if (action.IsSuccess) return Ok(AutomapperGeneric<TipoVia, TablaGlobalDto>.Map(newEntity));
        return BadRequest(action.Message);
    }

    #endregion HttpPost => Adiciona tipo de vía

    #region HttpPut => Actualiza tipo de vía

    /// <summary>
    /// Actualizar tipo de vía
    /// </summary>
    /// <param name="entityDto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> PutAsync(TablaGlobalDto entityDto)
    {
        var response = await _unitOfWork.GetAsync(entityDto.Id);
        if (!response.IsSuccess) return NotFound();

        var updateEntity = response.Result;
        if (updateEntity is null) return NotFound();

        updateEntity.Codigo = entityDto.Codigo;
        updateEntity.Name = entityDto.Name;
        updateEntity.AuditUpdateFecha = DateTime.Now;
        updateEntity.AuditUpdateUsuario = entityDto.AuditUsuario;

        var action = await _unitOfWork.UpdateAsync(updateEntity);
        if (action.IsSuccess) return Ok(AutomapperGeneric<TipoVia, TablaGlobalDto>.Map(updateEntity));
        return BadRequest(action.Message);
    }

    #endregion HttpPut => Actualiza tipo de vía

    #region HttpDelete => Elimina tipo de vía

    /// <summary>
    /// Elimina tipo de vía
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> DeletesAsync(int id)
    {
        var action = await _unitOfWork.DeleteAsync(id);
        if (action.IsSuccess) return NoContent();
        return BadRequest(action.Message);
    }

    #endregion HttpDelete => Elimina tipo de vía

    #region NonAction => Ignora método PostAsync

    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> PostAsync(TipoVia dto)
    {
        return Ok();
    }

    #endregion NonAction => Ignora método PostAsync

    #region NonAction => Ignora método PutAsync

    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> PutAsync(TipoVia dto)
    {
        return Ok();
    }

    #endregion NonAction => Ignora método PutAsync

    #region NonAction => Ignora método DeleteAsync

    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok();
    }

    #endregion NonAction => Ignora método DeleteAsync
}