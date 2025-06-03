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
public class TipoAlmacenController : GenericController<TipoAlmacen>
{
    #region Constructor

    private readonly IGenericUnitOfWork<TipoAlmacen> _unitOfWork;
    private readonly IMapper _mapper;

    public TipoAlmacenController(IGenericUnitOfWork<TipoAlmacen> unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #endregion Constructor

    #region HttpGet => Recuperar tipos de almacenes

    /// <summary>
    /// Recuperar tipos de almacenes
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _unitOfWork.GetAsync();
        if (response.IsSuccess) return Ok(AutomapperGeneric<TipoAlmacen, TablaGlobalSelectDto>.MapList(response.Result.ToList()));
        return BadRequest();
    }

    #endregion HttpGet => Recuperar tipos de almacenes

    #region HttpGet => Recuperar tipo de almacén por ID

    /// <summary>
    /// Recuperar tipo de almacén por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _unitOfWork.GetAsync(id);
        if (response.IsSuccess) return Ok(AutomapperGeneric<TipoAlmacen, TablaGlobalSelectDto>.Map(response.Result));
        return NotFound(response.Message);
    }

    #endregion HttpGet => Recuperar tipo de almacén por ID

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

    #region HttpGet => Recuperar tipo de almacén por página

    /// <summary>
    /// Recuperar tipo de almacén por página
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

    #endregion HttpGet => Recuperar tipo de almacén por página

    #region HttpPost => Adiciona tipo de almacén

    /// <summary>
    /// Adiciona tipo de almacén
    /// </summary>
    /// <param name="entityDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync(TablaGlobalDto entityDto)
    {
        var newEntity = AutomapperGeneric<TablaGlobalDto, TipoAlmacen>.Map(entityDto);
        newEntity.AuditInsertFecha = DateTime.Now;
        newEntity.AuditInsertUsuario = entityDto.AuditUsuario;

        var action = await _unitOfWork.AddAsync(newEntity);
        if (action.IsSuccess) return Ok(AutomapperGeneric<TipoAlmacen, TablaGlobalDto>.Map(newEntity));
        return BadRequest(action.Message);
    }

    #endregion HttpPost => Adiciona tipo de almacén

    #region HttpPut => Actualizar tipo de almacén

    /// <summary>
    /// Actualizar tipo de almacén
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
        updateEntity.EsActivo = entityDto.EsActivo;
        updateEntity.AuditUpdateFecha = DateTime.Now;
        updateEntity.AuditUpdateUsuario = entityDto.AuditUsuario;

        var action = await _unitOfWork.UpdateAsync(updateEntity);
        if (action.IsSuccess) return Ok(AutomapperGeneric<TipoAlmacen, TablaGlobalDto>.Map(updateEntity));
        return BadRequest(action.Message);
    }

    #endregion HttpPut => Actualizar tipo de almacén

    #region HttpDelete => Elimina tipo de almacén

    /// <summary>
    /// Elimina tipo de almacén
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

    #endregion HttpDelete => Elimina tipo de almacén

    #region NonAction => Ignora método PostAsync

    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> PostAsync(TipoAlmacen dto)
    {
        return Ok();
    }

    #endregion NonAction => Ignora método PostAsync

    #region NonAction => Ignora método PutAsync

    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> PutAsync(TipoAlmacen dto)
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