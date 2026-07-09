using Core.Entidade;
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
    }
}
