using Core.Entidade;

namespace WebAPI.Respository
{
    public interface TesteUsuarioRepository
    {
        public Task<List<Usuario>> GetAll();
       
    }
}
