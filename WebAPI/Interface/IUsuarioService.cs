using Core.Entidade.Enums;
using Core.Input;
using Core.Output;

namespace WebAPI.Interface
{
    public interface IUsuarioService
    {
        public Task<ServiceResponse<List<UsuarioListarResposta>>> Listar(string? nome = null, string? email = null, string? nivelAcessoId = null);

        public Task<ServiceResponse<UsuarioAdicionarResposta>> AdicionarUsuario(UsuarioAdicionarRequisicao usuarioInput, NivelAcessoEnum nivelAcesso);

        public Task<ServiceResponse<UsuarioBuscarPorIdResposta>> BuscarPorId(int id);

        public Task<ServiceResponse> AtualizarUsuario(UsuarioAtualizarRequisicao usuarioUpdate,int id);

        public Task<ServiceResponse> RemoverUsuario(int id);
        public Task<ServiceResponse<UsuarioBuscarAutenticadoResposta>> BuscarAutenticado(string email);
    }
}