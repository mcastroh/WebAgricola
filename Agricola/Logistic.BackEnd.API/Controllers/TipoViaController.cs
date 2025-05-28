using Azure;
using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Logistic.Shared.DTOs;
using Logistic.Shared.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

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

    //private readonly ICountriesUnitOfWork _countriesUnitOfWork;

    //public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork countriesUnitOfWork) : base(unitOfWork)
    //{
    //    _countriesUnitOfWork = countriesUnitOfWork;
    //}

    //private readonly ICountriesUnitOfWork _countriesUnitOfWork;

    //public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork countriesUnitOfWork) : base(unitOfWork)
    //{
    //    _countriesUnitOfWork = countriesUnitOfWork;
    //}

    #endregion Constructor

    //[HttpGet]
    //public async Task<IActionResult> GetAsync()
    //{
    //    var results = await _unitOfWork.GetAsync();
    //    if (results.Result.Count() == 0) return NotFound();
    //    //var response = AutomapperGeneric<TipoVia, GenericEntitySelectDto<TipoVia>>.MapList(results.ToList());

    //    return Ok(results);
    //}

    [HttpGet]
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

    //[AllowAnonymous]
    //[HttpGet("combo")]
    //public async Task<IActionResult> GetComboAsync()
    //{
    //    return Ok(await _unitOfWork.GetComboAsync());
    //}

    //#region HttpGet => Select All

    ///// <summary>
    ///// Recuperar tipos de vías
    ///// </summary>
    ///// <returns></returns>
    //[HttpGet]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<GenericEntitySelectDto<TipoVia>>>))]
    //[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //public async Task<IActionResult> gets()
    //{
    //    var results = await _unitOfWork.GetRepository<TipoVia>().GetAll();
    //    if (results.Count() == 0) return NotFound();
    //    var response = AutomapperGeneric<TipoVia, GenericEntitySelectDto<TipoVia>>.MapList(results.ToList());
    //    return Ok(response);
    //}

    //#endregion

    //#region HttpPost => CRUD: Add

    ///// <summary>
    ///// Adicionar tipo de vía
    ///// </summary>
    ///// <param name="entityDto"></param>
    ///// <returns></returns>
    //[HttpPost]
    //[ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ApiResponse<GenericEntitySelectDto<TipoVia>>))]
    //[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    //public async Task<IActionResult> posts(GenericEntityDto<TipoVia> entityDto)
    //{
    //    try
    //    {
    //        using var transaction = _unitOfWork.BeginTransaction();

    //        var newEntity = AutomapperGeneric<GenericEntityDto<TipoVia>, TipoVia>.Map(entityDto);
    //        newEntity.AuditInsertFecha = DateTime.Now;
    //        newEntity.AuditInsertUsuario = entityDto.AuditUsuario;

    //        await _unitOfWork.GetRepository<TipoVia>().Add(newEntity);
    //        await _unitOfWork.SaveChanges();
    //        await _unitOfWork.CommitTransaction();

    //        var response = AutomapperGeneric<TipoVia, GenericEntitySelectDto<TipoVia>>.Map(newEntity);
    //        return Created(string.Empty, response);
    //    }
    //    catch (Exception)
    //    {
    //        await _unitOfWork.RollBach();
    //        throw;
    //    }
    //}

    //#endregion

    //#region HttpPut => CRUD: Update

    ///// <summary>
    ///// Actualizar tipo de vía
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="entityDto"></param>
    ///// <returns></returns>
    //[HttpPut]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<GenericEntitySelectDto<TipoVia>>))]
    //[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //public async Task<IActionResult> put(int id, GenericEntityDto<TipoVia> entityDto)
    //{
    //    try
    //    {
    //        var updateEntity = await _unitOfWork.GetRepository<TipoVia>().GetById(id);
    //        if (updateEntity == null) return NotFound();

    //        using var transaction = _unitOfWork.BeginTransaction();

    //        updateEntity.Codigo = entityDto.Codigo;
    //        updateEntity.Name = entityDto.Name;
    //        updateEntity.EsActivo = entityDto.EsActivo;
    //        updateEntity.AuditUpdateFecha = DateTime.Now;
    //        updateEntity.AuditUpdateUsuario = entityDto.AuditUsuario;

    //        await _unitOfWork.GetRepository<TipoVia>().Update(updateEntity);
    //        await _unitOfWork.SaveChanges();
    //        await _unitOfWork.CommitTransaction();

    //        var response = AutomapperGeneric<TipoVia, GenericEntitySelectDto<TipoVia>>.Map(updateEntity);
    //        return Ok(response);
    //    }
    //    catch (Exception)
    //    {
    //        await _unitOfWork.RollBach();
    //        throw;
    //    }
    //}

    //#endregion

    //#region HttpDelete => CRUD: Delete

    ///// <summary>
    ///// Elimina tipo de vía
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpDelete("{id}")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
    //[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //public async Task<IActionResult> delete(int id)
    //{
    //    try
    //    {
    //        var deleteEntity = await _unitOfWork.GetRepository<TipoVia>().GetById(id);
    //        if (deleteEntity == null) return NotFound();

    //        using var transaction = _unitOfWork.BeginTransaction();

    //        await _unitOfWork.GetRepository<TipoVia>().Delete(id);
    //        await _unitOfWork.SaveChanges();
    //        await _unitOfWork.CommitTransaction();

    //        var response = new ApiResponse<bool>(true);
    //        return Ok(response);
    //    }
    //    catch (Exception)
    //    {
    //        await _unitOfWork.RollBach();
    //        throw;
    //    }
    //}

    //#endregion

    //#region HttpGet => Select By Id

    ///// <summary>
    ///// Recuperar tipo de vía por Id
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpGet("{id}")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<GenericEntitySelectDto<TipoVia>>))]
    //[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //public async Task<IActionResult> get(int id)
    //{
    //    var result = await _unitOfWork.GetRepository<TipoVia>().GetById(id);
    //    if (result == null) return NotFound();
    //    var response = AutomapperGeneric<TipoVia, GenericEntitySelectDto<TipoVia>>.Map(result);
    //    return Ok(response);
    //}

    //#endregion

    //#region HttpGet => Total Registros By Código

    ///// <summary>
    ///// Total tipo de vía por código
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="codigo"></param>
    ///// <returns></returns>
    //[HttpGet("{id}/{codigo}")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TotalRegistrosDto>))]
    //[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    //public async Task<IActionResult> get(int id, string codigo)
    //{
    //    var totReg = new TotalRegistrosDto();
    //    totReg.TotalRegistros = await _unitOfWork.GetRepository<TipoVia>().GetTotalRegistrosByFilters(x => x.Id != id && x.Codigo == codigo);
    //    return Ok(totReg);
    //}

    //#endregion
}