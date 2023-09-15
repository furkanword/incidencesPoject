using System.Net.NetworkInformation;

namespace Api.Dtos;

public class DatosUsuarioDto
{
    public string ? Mensaje { get; set; }
    public bool EstaAutenticado { get; set; }
    public string ? UserName { get; set; }
    public string ? Email { get; set; }
    public List<string> ? Roles { get; set; }
    public string ? AccessToken { get; set; }
    public string ? RefreshToken {get;set;}
    public DateTime Expiry {get;set;}
}
