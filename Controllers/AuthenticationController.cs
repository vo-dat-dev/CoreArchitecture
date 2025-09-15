using CoreArchitecture.Interface;
using CoreArchitecture.State;
using Microsoft.AspNetCore.Mvc;

namespace CoreArchitecture.Controllers;

[Route("authenticate")]
[ApiController]
public class AuthenticationController(IAuthentication authentication) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return Ok(await authentication.AuthenticationTest());
    }

    [HttpGet("test")]
    public IActionResult TestAuthentication()
    {
        // chúng ta không thể làm gì với authentication ở đây vì nó là một interface
        var order = new Order();
        order.Process();
        return Ok("test authentication");
    }
}