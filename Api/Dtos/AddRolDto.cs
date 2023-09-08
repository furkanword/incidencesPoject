using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class AddRolDto
{
    [Required]
    public string ? Username { get; set; }
    [Required]
    public string ? Password { get; set; }
    [Required]
    public string ? Role { get; set; }
}
