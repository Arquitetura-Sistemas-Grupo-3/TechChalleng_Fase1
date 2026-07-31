using Core.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Auth(string email, string password)
        {
            AuthReturn? data = await _authService.Login(email, password);

            if (data == null) return Unauthorized();

            return Ok(data);
        }

        [HttpGet("/admin")]
        [Authorize(Policy ="Admin")]
        public IActionResult Admin()
        {
            return Ok("Endpoint ADMIN");
        }
    }
}
