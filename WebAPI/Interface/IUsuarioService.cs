using Core.Entidade;
using Core.Input;

namespace WebAPI.Interface
{
    public interface IUsuarioService
    {
        public Task<IList<Usuario>> GetAll();
        public void AddUsuario(UsuarioInput usuarioInput);
    }
}
