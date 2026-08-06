using Core.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interface;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Autenticação e autorização de usuários.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autentica um usuário e retorna o token JWT de acesso.
        /// </summary>
        /// <param name="email">E-mail do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>Nome do usuário e token JWT.</returns>
        /// <response code="401">Credenciais inválidas.</response>
        [HttpGet]
        [ProducesResponseType(typeof(AuthReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Auth(string email, string password)
        {
            AuthReturn? data = await _authService.Login(email, password);

            if (data == null) return Unauthorized();

            return Ok(data);
        }

        /// <summary>
        /// Endpoint de exemplo restrito a usuários com a política "Admin".
        /// </summary>
        /// <returns>Mensagem de confirmação de acesso.</returns>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário autenticado sem permissão de Admin.</response>
        [HttpGet("/admin")]
        [Authorize(Policy ="Admin")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Admin()
        {
            return Ok("Endpoint ADMIN");
        }
    }
}
