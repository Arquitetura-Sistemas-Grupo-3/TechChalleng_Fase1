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
        [ProducesResponseType(typeof(ServiceResponse<AuthReturn>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Auth(string email, string password)
        {
            var response = await _authService.Login(email, password);

            if (!response.Success) return Unauthorized(response);

            return Ok(response);
        }
    }
}
