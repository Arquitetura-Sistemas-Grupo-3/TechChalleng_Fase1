using Core.Entidade;
using Core.Input;
using Core.Output;
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
    public class UsuarioController : Controller
    {
        private IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> ListarAsync([FromQuery] string? nome,[FromQuery] string? email,[FromQuery] string? nivelAcesso)
        {

            var usuarios = await _usuarioService.Listar(nome, email, nivelAcesso);
            return Ok(usuarios);
        }

        /// <summary>
        /// Consulta um usuário pelo identificador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Dados do usuário encontrado.</returns>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<UsuarioBuscarPorIdResposta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            var usuario = await _usuarioService.BuscarPorId(id);

            return Ok(usuario);
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="usuarioInput">Dados do usuário a ser criado.</param>
        /// <returns>Mensagem de confirmação do cadastro.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<UsuarioAdicionarResposta>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Adicionar([FromBody] UsuarioAdicionarRequisicao usuarioInput)
        {
            var usuario = await _usuarioService.AdicionarUsuario(usuarioInput,2);
            return CreatedAtAction("BuscarPorId", new { id = usuario.Data.Id }, usuario);
        }

        /// <summary>
        /// Cadastra um novo usuário Administrador.
        /// </summary>
        /// <param name="usuarioInput">Dados do usuário a ser criado.</param>
        /// <returns>Mensagem de confirmação do cadastro.</returns>
        [HttpPost("admin")]
        [ProducesResponseType(typeof(ServiceResponse<UsuarioAdicionarResposta>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AdicionarAdmin([FromBody] UsuarioAdicionarRequisicao usuarioInput)
        {
            var usuario = await _usuarioService.AdicionarUsuario(usuarioInput, 1);

            return CreatedAtAction("BuscarPorId", new { id = usuario.Data.Id }, usuario);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="usuarioUpdate">Novos dados do usuário (o Id deve ser informado no corpo da requisição).</param>
        /// <returns>Mensagem de confirmação da atualização.</returns>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Atualizar([FromBody] UsuarioAtualizarRequisicao usuarioUpdate, [FromRoute] int id)
        {
            var usuario = await _usuarioService.AtualizarUsuario(usuarioUpdate, id);
            return Ok(usuario);
        }

        /// <summary>
        /// Remove um usuário existente.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Mensagem de confirmação da exclusão.</returns>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Remover(int id)
        {
            var usuario = await _usuarioService.RemoverUsuario(id);
            return Ok(usuario);
        }

        /// <summary>
        /// Consulta o usuário autenticado.
        /// </summary>
        /// <returns>Dados do usuário encontrado.</returns>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("me")]
        [ProducesResponseType(typeof(ServiceResponse<UsuarioBuscarAutenticadoResposta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> BuscarAutenticado()
        {
            var usuario = await _usuarioService.BuscarAutenticado(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(usuario);
        }
    }
}
