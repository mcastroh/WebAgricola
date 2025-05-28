using AutoMapper;
using Logistic.BackEnd.API.Mappings;
using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Logistic.Shared.DTOs;
using Logistic.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

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
    [HttpGet("{id}")]
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

    /// <summary>
    /// Adicionar tipo de vía
    /// </summary>
    /// <param name="entityDto"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task<IActionResult> PostAsync(TablaGlobalDto entityDto)
    {
        var newEntity = AutomapperGeneric<TablaGlobalDto, TipoVia>.Map(entityDto);
        newEntity.AuditInsertFecha = DateTime.Now;
        newEntity.AuditInsertUsuario = entityDto.AuditUsuario;

        //dto.AdminId = User.Identity!.Name!;
        var action = await _unitOfWork.AddAsync(newEntity);
        if (action.IsSuccess) return Ok(action.Result);
        return BadRequest(action.Message);
    }

    /// <summary>
    /// Actualizar tipo de vía
    /// </summary>
    /// <param name="entityDto"></param>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(TablaGlobalDto entityDto)
    {
        var response = await _unitOfWork.GetAsync(entityDto.Id);
        if (response == null) return NotFound();

        var updateEntity = AutomapperGeneric<TablaGlobalDto, TipoVia>.Map(entityDto);

        updateEntity.Codigo = entityDto.Codigo;
        updateEntity.Name = entityDto.Name;
        updateEntity.EsActivo = entityDto.EsActivo;
        //updateEntity.AuditInsertFecha = response.Result.AuditInsertFecha;
        //updateEntity.AuditInsertUsuario = response.Result.AuditInsertUsuario;
        updateEntity.AuditUpdateFecha = DateTime.Now;
        updateEntity.AuditUpdateUsuario = entityDto.AuditUsuario;

        var action = await _unitOfWork.UpdateAsync(updateEntity);
        if (action.IsSuccess) return Ok(action.Result);
        return BadRequest(action.Message);
    }
}