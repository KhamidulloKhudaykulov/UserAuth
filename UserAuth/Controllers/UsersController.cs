using Microsoft.AspNetCore.Mvc;
using UserAuth.Model.Users;
using UserAuth.Models;
using UserAuth.Service.Services;

namespace UserAuth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] UserCreateModel model)
    {
        try
        {
            var user = await userService.CreateAsync(model);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAsync()
    {
        return Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.GetAllAsync()
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> LoginAsync([FromBody] string email, string password)
    {
        return Ok(new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.LoginAsync(email, password)
        });
    }
}
