using Core.Entidade;
using Core.Input;

namespace WebAPI.Interface
{
    public interface IUsuarioService
    {
        public Task<List<Usuario>> GetAll();
        public string AddUsuario(UsuarioInput usuarioInput);

        public Task<Usuario?> GetById(int id);

        public Task<string> UpdateUsuario(UsuarioUpdate usuarioUpdate);
        
        public Task<string> DeleteUsuario(int id);
    }
}
