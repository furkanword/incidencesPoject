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

public class PerisfericoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PerisfericoController(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<PerisfericoDto>>> Get()
    {
        var perisferico = await _unitOfWork.Perisfericos.GetAllAsync();
        return _mapper.Map<List<PerisfericoDto>>(perisferico);
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
    public async Task<ActionResult<PerisfericoDto>> Get(string id)
    {
        var perisferico = await _unitOfWork.Perisfericos.GetByIdAsync(id);
        if (perisferico == null){
            return NotFound();
        }
        return _mapper.Map<PerisfericoDto>(perisferico);
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
    public async Task<ActionResult<Pais>> Post(PerisfericoDto perisfericoDto){
        var perisferico = _mapper.Map<NivelIncidencia>(perisfericoDto);
        this._unitOfWork.NivelIncidencias.Add(perisferico);
        await _unitOfWork.SaveAsync();
        if (perisferico == null)
        {
            return BadRequest();
        }
        perisfericoDto.Id = perisferico.Id;
        return CreatedAtAction(nameof(Post),new {id= perisfericoDto.Id}, perisfericoDto);
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
    public async Task<ActionResult<PerisfericoDto>> Put(int id, [FromBody]PerisfericoDto perisfericoDto){
        if(perisfericoDto == null)
            return NotFound();
        var perisfericos = _mapper.Map<Perisferico>(perisfericoDto);
        _unitOfWork.Perisfericos.Update(perisfericos);
        await _unitOfWork.SaveAsync();
        return perisfericoDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string  id){
        var perisferico = await _unitOfWork.Perisfericos.GetByIdAsync(id);
        if(perisferico == null){
            return NotFound();
        }
        _unitOfWork.Perisfericos.Remove(perisferico);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 