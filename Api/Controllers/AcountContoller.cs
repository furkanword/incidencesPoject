using Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using ApiIncidencias.Services;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]

public class AcountController : BaseApiController
{
    private readonly IUserService _UserServices;
        
    public AcountController(IUserService userServices) => _UserServices = userServices;
   
   
    [MapToApiVersion("1.0")]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto model) => Ok(await _UserServices.RegisterAsync(model));

    [MapToApiVersion("1.0")]
    [HttpPost("token")]    
    public async Task<ActionResult> GetTokenAsync(LoginDto model) => Ok(await _UserServices.GetTokenAsync(model));

    [MapToApiVersion("1.0")]
    [HttpPost("addrol")]
    
    public async Task<ActionResult> AddRoleAsync(AddRolDto model) => Ok(await _UserServices.AddRoleAsync(model));

     [HttpPost("refresh")]
    public async Task<IActionResult> GetTokenAsync(AuthenticationTokenResultDto model)
    {
        var result = await _UserServices.GetTokenAsync(model);
        return Ok(result);
    }

    
}


