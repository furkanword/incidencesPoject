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

public class PersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonaController(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<PersonaDto>>> Get()
    {
        var persona = await _unitOfWork.Personas.GetAllAsync();
        return _mapper.Map<List<PersonaDto>>(persona);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PersonaxIncidenciaDto>>> Get11([FromQuery] Params personaParams)
    {
        var persona = await _unitOfWork.Personas.GetAllAsync(personaParams.PageIndex,personaParams.PageSize,personaParams.Search);
        var lstpersonasDto = _mapper.Map<List<PersonaxIncidenciaDto>>(persona.registros);
        return new Pager<PersonaxIncidenciaDto>(lstpersonasDto,persona.totalRegistros,personaParams.PageIndex,personaParams.PageSize,personaParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaisDto>> Get(string id)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null){
            return NotFound();
        }
        return _mapper.Map<PaisDto>(persona);
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
    public async Task<ActionResult<Pais>> Post(PersonaDto personaDto){
        var personas = _mapper.Map<Persona>(personaDto);
        this._unitOfWork.Personas.Add(personas);
        await _unitOfWork.SaveAsync();
        if (personas == null)
        {
            return BadRequest();
        }
        personaDto.Id = personas.Id;
        return CreatedAtAction(nameof(Post),new {id= personaDto.Id}, personaDto);
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
    public async Task<ActionResult<PersonaDto>> Put(int id, [FromBody]PersonaDto personaDto){
        if(personaDto == null)
            return NotFound();
        var personas = _mapper.Map<Persona>(personaDto);
        _unitOfWork.Personas.Update(personas);
        await _unitOfWork.SaveAsync();
        return personaDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string  id){
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if(persona == null){
            return NotFound();
        }
        _unitOfWork.Personas.Remove(persona);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 