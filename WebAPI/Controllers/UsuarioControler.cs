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
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var usuarios = await _usuarioService.GetAll();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetById(id);
            
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] UsuarioInput usuarioInput)
        {
            try
            {
                var usuario = _usuarioService.AddUsuario(usuarioInput);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UsuarioUpdate usuarioUpdate)
        {
            try
            {
                var usuario = await _usuarioService.UpdateUsuario(usuarioUpdate);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var usuario = _usuarioService.DeleteUsuario(id);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
