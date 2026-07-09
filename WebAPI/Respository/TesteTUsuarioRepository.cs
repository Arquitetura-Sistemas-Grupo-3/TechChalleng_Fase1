using Core.Entidade;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Respository
{
    public class TesteTUsuarioRepository : TesteUsuarioRepository
    {
        private readonly ApplicationDbContext _usuarios;
       
        public TesteTUsuarioRepository(ApplicationDbContext applicationDbContext) 
        {
            _usuarios = applicationDbContext;
        }
        public async Task<List<Usuario>> GetAll()
        {
            return await _usuarios.Usuario.ToListAsync();
        }
    }
}
