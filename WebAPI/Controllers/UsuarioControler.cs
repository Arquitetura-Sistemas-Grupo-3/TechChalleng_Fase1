using Core.Entidade;
using Core.Input;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioControler : Controller
    {

        private IUsuarioRepository _usuarioRepository;
        private IUsuarioService _usuarioService;
        public UsuarioControler(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService)
        {

            _usuarioRepository = usuarioRepository;
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
            //var usuarios = await usuarioService.GetAll();
            //return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetById(id);
                if (usuario == null)
                {
                    return NotFound();
                }
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
                var usuario = new Usuario
                {
                    Nome = usuarioInput.Nome,
                    Email = usuarioInput.Email,
                    Senha = usuarioInput.Senha,
                    NivelAcessoId = usuarioInput.NivelAcessoId
                };

                _usuarioRepository.Add(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] UsuarioUpdate usuarioUpdate)
        {
            try
            {
                var usuario = new Usuario
                {
                    Id = usuarioUpdate.Id,
                    Nome = usuarioUpdate.Nome,
                    Email = usuarioUpdate.Email,
                    Senha = usuarioUpdate.Senha
                };
                _usuarioRepository.Update(usuario);
                return Ok();
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }
        }
    }
}
