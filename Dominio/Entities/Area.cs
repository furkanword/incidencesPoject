using System.ComponentModel.DataAnnotations;
using Dominio;

namespace Entities;

public class Area : BaseEntity
{
    public ICollection<Incidencia> ? Incidencias { get; set; }
    
    public ICollection<Lugar> ? Places { get; set; }
    public string ? Name_Area { get; set; }
    public ICollection<AreaUsuario> ? AreaUsuarios { get; set; }
    public string ? Description_Area { get; set; }
}
