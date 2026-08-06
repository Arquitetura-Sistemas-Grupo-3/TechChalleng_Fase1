using Core.Entidade;
using Core.Input;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interface;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioControler : Controller
    {
        private IUsuarioService _usuarioService;
        public UsuarioControler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? nome,[FromQuery] string? email,[FromQuery] string? nivelAcesso) 
        {

            var usuarios = await _usuarioService.GetAll(nome, email, nivelAcesso);
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var usuario = await _usuarioService.GetById(id);

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UsuarioInput usuarioInput)
        {
            var usuario = _usuarioService.AddUsuario(usuarioInput);

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UsuarioUpdate usuarioUpdate,int id)
        {
            var usuario = await _usuarioService.UpdateUsuario(usuarioUpdate, id);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _usuarioService.DeleteUsuario(id);
            return Ok(usuario);
        }
    }
}
