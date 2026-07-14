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
            try
            {
                AuthReturn? data = await _authService.Login(email, password);

                if (data == null) return Unauthorized();

                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("/admin")]
        [Authorize(Policy ="Admin")]
        public IActionResult Admin()
        {
            try
            {
                return Ok("Endpoint ADMIN");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
