using BC = BCrypt.Net.BCrypt;
using Core.Entidade;
using Core.Input;
using Infra.Repository;
using WebAPI.Interface;
using WebAPI.Respository;

namespace WebAPI.Service
{
    public class UsuarioService : IUsuarioService
    {
        private TesteUsuarioRepository usuarioRepository;

        private IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IList<Usuario>> GetAll()
        {
            return await _usuarioRepository.GetAll();
        }

        public void AddUsuario(UsuarioInput usuarioInput)
        {
            Usuario usuario = new Usuario
            {
                Nome = usuarioInput.Nome,
                Email = usuarioInput.Email,
                Senha = BC.HashPassword(usuarioInput.Senha),
                NivelAcessoId = usuarioInput.NivelAcessoId
            };

            _usuarioRepository.Add(usuario);
        }
    }
}
