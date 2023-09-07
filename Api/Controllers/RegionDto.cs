using Api.Dtos;
using ApiIncidencias.Helpers;
using AutoMapper;
using Dominio;
using Dominio.Interfaces;
using Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class RegionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
   /* [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<Area>>> Get()
    {
        var regiones = await _unitOfWork.Areas.GetAllAsync();
        return Ok(regiones);
    }*/
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<RegionDto>>> Get()
    {
        var region = await _unitOfWork.Regiones.GetAllAsync();
        return _mapper.Map<List<RegionDto>>(region);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RegionxCiudadDto>>> Get11([FromQuery] Params regionaParams)
    {
        var region = await _unitOfWork.Regiones.GetAllAsync(regionaParams.PageIndex,regionaParams.PageSize,regionaParams.Search);
        var lstregionDto = _mapper.Map<List<RegionxCiudadDto>>(region.registros);
        return new Pager<RegionxCiudadDto>(lstregionDto,region.totalRegistros,regionaParams.PageIndex,regionaParams.PageSize,regionaParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegionDto>> Get(string id)
    {
        var region = await _unitOfWork.Regiones.GetByIdAsync(id);
        if (region == null){
            return NotFound();
        }
        return _mapper.Map<RegionDto>(region);
    }
    /*[HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Area>> Post(Area area){
        this._unitOfWork.Areas.Add(area);
        await _unitOfWork.SaveAsync();
        if (area == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id= area.Id}, area);
    }*/
 
    /*[HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Area>> Put(int id, [FromBody]Area area){
        if(area == null)
            return NotFound();
        _unitOfWork.Areas.Update(area);
        await _unitOfWork.SaveAsync();
        return area;
        
    }*/
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RegionDto>> Put(int id, [FromBody]RegionDto regionDto){
        if(regionDto == null)
            return NotFound();
        var regiones = _mapper.Map<Region>(regionDto);
        _unitOfWork.Regiones.Update(regiones);
        await _unitOfWork.SaveAsync();
        return regionDto;   
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string  id){
        var region = await _unitOfWork.Regiones.GetByIdAsync(id);
        if(region == null){
            return NotFound();
        }
        _unitOfWork.Regiones.Remove(region);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 