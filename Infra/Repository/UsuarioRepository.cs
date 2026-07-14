using Core.Entidade;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext applicationDb) : base(applicationDb)
        {
        }

        public async Task<Usuario?> ValidaEmailSenha(string email, string senha)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }
    }
}
