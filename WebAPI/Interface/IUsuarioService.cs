using Core.Entidade;
using Core.Input;

namespace WebAPI.Interface
{
    public interface IUsuarioService
    {
        public Task<List<Usuario>> GetAll(string? nome = null, string? email = null, int? nivelAcessoId = null);
        public string AddUsuario(UsuarioInput usuarioInput);

        public Task<Usuario?> GetById(int id);

        public Task<string> UpdateUsuario(UsuarioUpdate usuarioUpdate);
        
        public Task<string> DeleteUsuario(int id);
    }
}
