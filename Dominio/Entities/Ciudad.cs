namespace Dominio;

public class Ciudad : BaseEntity
{
public ICollection<Direccion> ? Direcciones { get; set; }

public string ?NameCity { get; set; }
public int ?Id_Region { get; set; }
public Region ? Region { get; set; }

}