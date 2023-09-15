using System.ComponentModel.DataAnnotations;
using Dominio;
using iText.Kernel.Crypto.Securityhandler;

namespace Entities;

public class Rol : BaseEntity
{
    public string ?Name_Rol { get; set; }
    public ICollection<Usuario> ?Usuario { get; set; }
    public string ?Description_Rol { get; set; }
    public ICollection<UsuarioRol> ? UsuarioRoles {get;set;}
}