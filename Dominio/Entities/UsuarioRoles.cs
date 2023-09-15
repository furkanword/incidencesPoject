using System.ComponentModel.DataAnnotations;
using Entities;

namespace Dominio;

public class UsuarioRol : BaseEntity
{
    public int UserId {get;set;}
    public Usuario ? Usuario { get; set; }
    public int RolId { get; set; }
    public Rol ? Rol { get; set; }
}
