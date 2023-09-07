using Api.Dtos;
using AutoMapper;
using Dominio.Interfaces;
using Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class NivelIncidenciaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NivelIncidenciaController(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<NivelIncidenciaDto>>> Get()
    {
        var nivelesIncidencia = await _unitOfWork.NivelIncidencias.GetAllAsync();
        return _mapper.Map<List<NivelIncidenciaDto>>(nivelesIncidencia);
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
    public async Task<ActionResult<NivelIncidenciaDto>> Get(string id)
    {
        var nivelincidencia = await _unitOfWork.NivelIncidencias.GetByIdAsync(id);
        if (nivelincidencia == null){
            return NotFound();
        }
        return _mapper.Map<NivelIncidenciaDto>(nivelincidencia);
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
    public async Task<ActionResult<NivelIncidencia>> Post(NivelIncidenciaDto nivelIncidenciaDto){
        var nivelincidencias = _mapper.Map<NivelIncidencia>(nivelIncidenciaDto);
        this._unitOfWork.NivelIncidencias.Add(nivelincidencias);
        await _unitOfWork.SaveAsync();
        if (nivelincidencias == null)
        {
            return BadRequest();
        }
        nivelIncidenciaDto.Id = nivelincidencias.Id;
        return CreatedAtAction(nameof(Post),new {id= nivelIncidenciaDto.Id}, nivelIncidenciaDto);
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
    public async Task<ActionResult<NivelIncidenciaDto>> Put(int id, [FromBody]NivelIncidenciaDto nivelIncidenciaDto){
        if(nivelIncidenciaDto == null)
            return NotFound();
        var nivelesincidencia = _mapper.Map<NivelIncidencia>(nivelIncidenciaDto);
        _unitOfWork.NivelIncidencias.Update(nivelesincidencia);
        await _unitOfWork.SaveAsync();
        return nivelIncidenciaDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string  id){
        var nivelincidencia = await _unitOfWork.NivelIncidencias.GetByIdAsync(id);
        if(nivelincidencia == null){
            return NotFound();
        }
        _unitOfWork.NivelIncidencias.Remove(nivelincidencia);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 