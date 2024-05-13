using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAuth.Model.Users;
using UserAuth.Service.Services;

namespace UserAuth.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> Register([FromBody] UserCreateModel model)
    {
        return Ok(await userService.CreateAsync(model));
    }

    [HttpGet("{email}={password}")]
    public async ValueTask<IActionResult> Login(string email, string password)
    {
        return Ok(await userService.LoginWithTokenAsync(email, password));
    }

    [HttpGet("{id}"), Authorize(Roles = "Admin")]
    public async ValueTask<IActionResult> GetById(long id)
    {
        return Ok(await userService.GetAsync(id));
    }
}
