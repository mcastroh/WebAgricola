using AutoMapper;
using Logistic.BackEnd.API.Mappings;
using Logistic.BackEnd.API.Models;
using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Logistic.Shared.DTOs;
using Logistic.Shared.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Logistic.BackEnd.API.Controllers;

//[Authorize]
//[Produces("application/json")]
//[Route("api/[controller]")]
//[ApiController]

[ApiController]
[Route("api/[controller]")]
public class XTipoViaController : GenericController<TipoVia>
{
    #region Constructor

    private readonly IGenericUnitOfWork<TipoVia> _unitOfWork;
    private readonly IMapper _mapper;

    public XTipoViaController(IGenericUnitOfWork<TipoVia> unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #endregion Constructor

    /// <summary>
    /// Recuperar tipos de vías
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _unitOfWork.GetAsync();
        if (response.IsSuccess) return Ok(response.Result);
        return BadRequest();
    }

    /// <summary>
    /// Recuperar tipo de vía por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _unitOfWork.GetAsync(id);
        if (response.IsSuccess) return Ok(response.Result);
        return NotFound(response.Message);
    }

    /// <summary>
    /// Recuperar tipo de vía por página
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    [HttpGet("totalRecordsPaginated")]
    public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _unitOfWork.GetAsync(pagination);
        if (action.IsSuccess) return Ok(action.Result);
        return BadRequest();
    }

    /// <summary>
    /// Adicionar tipo de vía
    /// </summary>
    /// <param name="entityDto"></param>
    /// <returns></returns>
    [HttpPost("full")]
    public async Task<IActionResult> PostAsync(TablaGlobalDto entityDto)
    {
        // var newEntity = AutomapperGeneric<TablaGlobalDto<TipoVia>, TipoVia>.Map(entityDto);
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
    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(TablaGlobalDto entityDto)
    {
        var response = await _unitOfWork.GetAsync(entityDto.Id);
        if (response == null) return NotFound();

        var updateEntity = AutomapperGeneric<TablaGlobalDto, TipoVia>.Map(entityDto);

        updateEntity.Codigo = entityDto.Codigo;
        updateEntity.Name = entityDto.Name;
        updateEntity.EsActivo = entityDto.EsActivo;
        updateEntity.AuditInsertFecha = response.Result.AuditInsertFecha;
        updateEntity.AuditInsertUsuario = response.Result.AuditInsertUsuario;
        updateEntity.AuditUpdateFecha = DateTime.Now;
        updateEntity.AuditUpdateUsuario = entityDto.AuditUsuario;

        var action = await _unitOfWork.UpdateAsync(updateEntity);
        if (action.IsSuccess) return Ok(action.Result);
        return BadRequest(action.Message);
    }
}