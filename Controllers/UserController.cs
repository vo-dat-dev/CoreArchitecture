using CoreArchitecture.Interface;
using CoreArchitecture.Models;
using CoreArchitecture.Reposititories;
using Microsoft.AspNetCore.Mvc;

namespace CoreArchitecture.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController(IAuthentication authentication, IUserRepository userRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(User newUser)
        {
            return Ok(userRepository.AddAsync(newUser));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            return Ok(await authentication.AuthenticationTest());
        }
    }
}