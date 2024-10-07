using ApiUniversidad.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiUniversidad.Controllers;

[Route("/roles")]
[ApiController] 
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("/getAllRoles")]
    public async Task<IActionResult> getAllRoles()
    {
        var roles = await _roleService.getAllRoles();
        return Ok(roles);
    }
}