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

public class TipoDocumentoCotroller : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoDocumentoCotroller(IUnitOfWork unitOfWork, IMapper mapper)
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
    public async  Task<ActionResult<IEnumerable<TipoDocumentoDto>>> Get()
    {
        var tipodocumento = await _unitOfWork.TipoDocumentos.GetAllAsync();
        return _mapper.Map<List<TipoDocumentoDto>>(tipodocumento);
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
    public async Task<ActionResult<TipoDocumentoDto>> Get(int id)
    {
        var tipodocumento = await _unitOfWork.TipoDocumentos.GetByIdAsync(id);
        if (tipodocumento == null){
            return NotFound();
        }
        return _mapper.Map<TipoDocumentoDto>(tipodocumento);
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
    public async Task<ActionResult<TipoDocumento>> Post(TipoDocumentoDto tipoDocumentoDto){
        var tipodocumento = _mapper.Map<TipoDocumento>(tipoDocumentoDto);
        this._unitOfWork.TipoDocumentos.Add(tipodocumento);
        await _unitOfWork.SaveAsync();
        if (tipodocumento == null)
        {
            return BadRequest();
        }
        tipoDocumentoDto.Id = tipodocumento.Id;
        return CreatedAtAction(nameof(Post),new {id= tipoDocumentoDto.Id}, tipoDocumentoDto);
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