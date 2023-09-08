using Api.Dtos;
using AutoMapper;
using Dominio.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class CategoriaContactoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriaContactoController(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<CategoriaContacto>>> Get()
    {
        var categoriascontactos = await _unitOfWork.CategoriaContactos.GetAllAsync();
        return _mapper.Map<List<CategoriaContacto>>(categoriascontactos);
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
    public async Task<ActionResult<CategoriaContactoDto>> Get(int id)
    {
        var categoriacontacto = await _unitOfWork.CategoriaContactos.GetByIdAsync(id);
        if (categoriacontacto == null){
            return NotFound();
        }
        return _mapper.Map<CategoriaContactoDto>(categoriacontacto);
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
    public async Task<ActionResult<CategoriaContacto>> Post(CategoriaContactoDto categoriaContactoDto){
        var categoriacontacto = _mapper.Map<CategoriaContacto>(categoriaContactoDto);
        this._unitOfWork.CategoriaContactos.Add(categoriacontacto);
        await _unitOfWork.SaveAsync();
        if (categoriacontacto == null)
        {
            return BadRequest();
        }
        categoriaContactoDto.Id_Category = categoriacontacto.Id;
        return CreatedAtAction(nameof(Post),new {id= categoriaContactoDto.Id_Category}, categoriaContactoDto);
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
    public async Task<ActionResult<CategoriaContactoDto>> Put(int id, [FromBody]CategoriaContactoDto categoriaContactoDto){
        if(categoriaContactoDto == null)
            return NotFound();
        var categoriaContactos = _mapper.Map<CategoriaContacto>(categoriaContactoDto);
        _unitOfWork.CategoriaContactos.Update(categoriaContactos);
        await _unitOfWork.SaveAsync();
        return categoriaContactoDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int  id){
        var categoriacontacto = await _unitOfWork.CategoriaContactos.GetByIdAsync(id);
        if(categoriacontacto == null){
            return NotFound();
        }
        _unitOfWork.CategoriaContactos.Remove(categoriacontacto);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 