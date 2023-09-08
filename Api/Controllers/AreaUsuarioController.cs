using Api.Dtos;
using AutoMapper;
using Dominio.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class AreaUsuarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AreaUsuarioController(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<AreaUsuariosDto>>> Get()
    {
        var AreasUsuarios = await _unitOfWork.AreaUsuarios.GetAllAsync();
        return _mapper.Map<List<AreaUsuariosDto>>(AreasUsuarios);
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
    public async Task<ActionResult<AreaUsuariosDto>> Get(int id)
    {
        var areausuario = await _unitOfWork.AreaUsuarios.GetByIdAsync(id);
        if (areausuario == null){
            return NotFound();
        }
        return _mapper.Map<AreaUsuariosDto>(areausuario);
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
    public async Task<ActionResult<AreaUsuario>> Post(AreaUsuariosDto areausuarioDto){
        var areaUsuario = _mapper.Map<AreaUsuario>(areausuarioDto);
        this._unitOfWork.AreaUsuarios.Add(areaUsuario);
        await _unitOfWork.SaveAsync();
        if (areaUsuario == null)
        {
            return BadRequest();
        }
        areausuarioDto.Id_A_P = areaUsuario.Id;
        return CreatedAtAction(nameof(Post),new {id= areausuarioDto.Id_A_P}, areausuarioDto);
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
    public async Task<ActionResult<AreaUsuariosDto>> Put(int id, [FromBody]AreaUsuariosDto areausuarioDto){
        if(areausuarioDto == null)
            return NotFound();
        var areasUsuarios = _mapper.Map<AreaUsuario>(areausuarioDto);
        _unitOfWork.AreaUsuarios.Update(areasUsuarios);
        await _unitOfWork.SaveAsync();
        return areausuarioDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int  id){
        var areausuario = await _unitOfWork.AreaUsuarios.GetByIdAsync(id);
        if(areausuario == null){
            return NotFound();
        }
        _unitOfWork.AreaUsuarios.Remove(areausuario);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
} 