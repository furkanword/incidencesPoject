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

public class DireccionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DireccionController(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<DireccionDto>>> Get()
    {
        var direccion = await _unitOfWork.Direcciones.GetAllAsync();
        return _mapper.Map<List<DireccionDto>>(direccion);
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
    public async Task<ActionResult<DireccionDto>> Get(string id)
    {
        var direccion = await _unitOfWork.Direcciones.GetByIdAsync(id);
        if (direccion == null){
            return NotFound();
        }
        return _mapper.Map<DireccionDto>(direccion);
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
    public async Task<ActionResult<Direccion>> Post(DireccionDto direccionDto){
        var direccion = _mapper.Map<Estado>(direccionDto);
        this._unitOfWork.Estados.Add(direccion);
        await _unitOfWork.SaveAsync();
        if (direccion == null)
        {
            return BadRequest();
        }
        direccionDto.Id = direccion.Id;
        return CreatedAtAction(nameof(Post),new {id= direccionDto.Id}, direccionDto);
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
    public async Task<ActionResult<DireccionDto>> Put(int id, [FromBody]DireccionDto direccionDto){
        if(direccionDto == null)
            return NotFound();
        var direcciones = _mapper.Map<Direccion>(direccionDto);
        _unitOfWork.Direcciones.Update(direcciones);
        await _unitOfWork.SaveAsync();
        return direccionDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string  id){
        var direccion = await _unitOfWork.Direcciones.GetByIdAsync(id);
        if(direccion == null){
            return NotFound();
        }
        _unitOfWork.Direcciones.Remove(direccion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 