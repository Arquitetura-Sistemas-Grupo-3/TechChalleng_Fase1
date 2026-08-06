using Core.Entidade;
using Core.Input;
using Core.Output;
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

        public async Task<Usuario?> ValidaEmailSenha(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<UsuarioReturn>> GetAllUsuario()
        {
            return await _dbSet
                .Include(u => u.NivelAcesso)
                .Select(u => new UsuarioReturn { Id= u.Id, Nome = u.Nome, Email =u.Email,DataCriacao = u.Data, NivelAcesso = u.NivelAcesso.Nome })
                .ToListAsync();
              
        }

        public async Task<UsuarioReturn?> GetUsuarioById(int id)
        {
            return await _dbSet
                .Select(u => new UsuarioReturn { Id = u.Id, Nome = u.Nome, Email = u.Email, DataCriacao = u.Data, NivelAcesso = u.NivelAcesso.Nome })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
