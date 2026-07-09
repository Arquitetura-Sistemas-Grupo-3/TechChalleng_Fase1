using Core.Entidade;

namespace WebAPI.Interface
{
    public interface IUsuarioService
    {
        public Task<IList<Usuario>> GetAll();
    }
}
