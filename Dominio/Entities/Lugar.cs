using System.ComponentModel.DataAnnotations;
using Dominio;

namespace Entities;

public class Lugar : BaseEntity
{
    public string ?Name_Place { get; set; }
    public ICollection<Incidencia> ?Incidencias { get; set; }
    public string ? Description_Place {get;set;}
    public int ?Id_AreaOrigin { get; set; }
    public Area ? Area { get; set; }
           
}