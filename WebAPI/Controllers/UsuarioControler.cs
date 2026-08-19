using Core.Entidade;
using Core.Input;
using Infra.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Interface;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Operações de gerenciamento de usuários.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UsuarioControler : Controller
    {
        private IUsuarioService _usuarioService;
        public UsuarioControler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? nome,[FromQuery] string? email,[FromQuery] string? nivelAcesso) 
        {

            var usuarios = await _usuarioService.GetAll(nome, email, nivelAcesso);
            return Ok(usuarios);
        }

        /// <summary>
        /// Consulta um usuário pelo identificador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Dados do usuário encontrado.</returns>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var usuario = await _usuarioService.GetById(id);

            return Ok(usuario);
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="usuarioInput">Dados do usuário a ser criado.</param>
        /// <returns>Mensagem de confirmação do cadastro.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] UsuarioInput usuarioInput)
        {
            var usuario = await _usuarioService.AddUsuario(usuarioInput);

            return Ok(usuario);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="usuarioUpdate">Novos dados do usuário (o Id deve ser informado no corpo da requisição).</param>
        /// <returns>Mensagem de confirmação da atualização.</returns>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UsuarioUpdate usuarioUpdate, [FromRoute] int id)
        {
            var usuario = await _usuarioService.UpdateUsuario(usuarioUpdate, id);
            return Ok(usuario);
        }

        /// <summary>
        /// Remove um usuário existente.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Mensagem de confirmação da exclusão.</returns>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioService.DeleteUsuario(id);
            return Ok(usuario);
        }

        /// <summary>
        /// Consulta o usuário autenticado.
        /// </summary>
        /// <returns>Dados do usuário encontrado.</returns>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("me")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var username = await _usuarioService.GetMe(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(new { username });
        }
    }
}
