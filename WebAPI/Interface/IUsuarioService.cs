using Core.Entidade;
using Core.Input;
using Core.Output;

namespace WebAPI.Interface
{
    public interface IUsuarioService
    {
        public Task<List<UsuarioReturn>> GetAll(string? nome = null, string? email = null, int? nivelAcessoId = null);
        
        public string AddUsuario(UsuarioInput usuarioInput);

        public Task<UsuarioReturn?> GetById(int id);

        public Task<string> UpdateUsuario(UsuarioUpdate usuarioUpdate,int id);
        
        public Task<string> DeleteUsuario(int id);
    }
}
