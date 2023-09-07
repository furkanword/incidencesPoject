using System.ComponentModel.DataAnnotations;
using Entities;

namespace Dominio;

public class Persona : BaseEntity
{
    public ICollection<Direccion> ?Direcciones { get; set; }
    public ICollection<Contacto> ?Contactos { get; set; }
    public ICollection<AreaUsuario> ? AreaUsuarios { get; set; }
    public ICollection<Incidencia> ?Incidencias { get; set; }
    public string ?Name { get; set; }
    public string ?Lastname { get; set; }
    public int Id_DocumentType { get; set; } 
    public TipoDocumento ? TipoDocumento { get; set; }
}