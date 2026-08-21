using Core.Entidade;
using Core.Input;
using Core.Output;

namespace WebAPI.Interface
{
    public interface IUsuarioService
    {
        public Task<ServiceResponse<List<UsuarioGetAllReturn>>> GetAll(string? nome = null, string? email = null, string? nivelAcessoId = null);

        public Task<ServiceResponse<UsuarioAddReturn>> AddUsuario(UsuarioInput usuarioInput,int nivelAcessoId);

        public Task<ServiceResponse<UsuarioGetByIdReturn>> GetById(int id);

        public Task<ServiceResponse> UpdateUsuario(UsuarioUpdate usuarioUpdate,int id);

        public Task<ServiceResponse> DeleteUsuario(int id);
        public Task<ServiceResponse<UsuarioMeReturn>> GetMe(string email);
    }
}
