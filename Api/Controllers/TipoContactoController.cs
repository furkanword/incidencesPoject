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

public class TipoContactoCotroller : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoContactoCotroller(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<TipoContactoDto>>> Get()
    {
        var tipocontacto = await _unitOfWork.TipoContactos.GetAllAsync();
        return _mapper.Map<List<TipoContactoDto>>(tipocontacto);
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
    public async Task<ActionResult<TipoContactoDto>> Get(string id)
    {
        var tipocontacto = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if (tipocontacto == null){
            return NotFound();
        }
        return _mapper.Map<TipoContactoDto>(tipocontacto);
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
    public async Task<ActionResult<TipoContacto>> Post(TipoContactoDto tipoContactoDto){
        var tipocontacto = _mapper.Map<TipoContacto>(tipoContactoDto);
        this._unitOfWork.TipoContactos.Add(tipocontacto);
        await _unitOfWork.SaveAsync();
        if (tipocontacto == null)
        {
            return BadRequest();
        }
        tipoContactoDto.Id = tipocontacto.Id;
        return CreatedAtAction(nameof(Post),new {id= tipoContactoDto.Id}, tipoContactoDto);
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
    public async Task<ActionResult<TipoContactoDto>> Put(int id, [FromBody]TipoContactoDto tipoContactoDto){
        if(tipoContactoDto == null)
            return NotFound();
        var tipoContactos = _mapper.Map<TipoContacto>(tipoContactoDto);
        _unitOfWork.TipoContactos.Update(tipoContactos);
        await _unitOfWork.SaveAsync();
        return tipoContactoDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string  id){
        var tipocontacto = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if(tipocontacto == null){
            return NotFound();
        }
        _unitOfWork.TipoContactos.Remove(tipocontacto);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 