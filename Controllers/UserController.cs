using AuthenticationApi.Interface;
using AuthenticationApi.Models;
using AuthenticationApi.Reposititories;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
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