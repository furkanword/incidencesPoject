using Api.Dtos;
using AutoMapper;
using Dominio;
using Dominio.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class TipoIncidenciaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoIncidenciaController(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<TipoIncidencia>>> Get()
    {
        var tipoincidencia = await _unitOfWork.TiposIncidencias.GetAllAsync();
        return _mapper.Map<List<TipoIncidencia>>(tipoincidencia);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
   
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoIncidenciaDto>> Get(int id)
    {
        var tipocontacto = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if (tipocontacto == null){
            return NotFound();
        }
        return _mapper.Map<TipoIncidenciaDto>(tipocontacto);
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
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoIncidencia>> Post(TipoIncidenciaDto tipoIncidenciaDto){
        var tipoIncidencia = _mapper.Map<CategoriaContacto>(tipoIncidenciaDto);
        this._unitOfWork.CategoriaContactos.Add(tipoIncidencia);
        await _unitOfWork.SaveAsync();
        if (tipoIncidencia == null)
        {
            return BadRequest();
        }
        tipoIncidenciaDto.Id = tipoIncidencia.Id;
        return CreatedAtAction(nameof(Post),new {id= tipoIncidenciaDto.Id}, tipoIncidenciaDto);
    }
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
    public async Task<ActionResult<TipoIncidenciaDto>> Put(int id, [FromBody]TipoIncidenciaDto tipoIncidenciaDto){
        if(tipoIncidenciaDto == null)
            return NotFound();
        var tipocontactos = _mapper.Map<TipoContacto>(tipoIncidenciaDto);
        _unitOfWork.TipoContactos.Update(tipocontactos);
        await _unitOfWork.SaveAsync();
        return tipoIncidenciaDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int  id){
        var tipocontacto = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if(tipocontacto == null){
            return NotFound();
        }
        _unitOfWork.TipoContactos.Remove(tipocontacto);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 