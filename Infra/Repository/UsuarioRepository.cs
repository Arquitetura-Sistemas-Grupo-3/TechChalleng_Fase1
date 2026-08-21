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
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && u.Ativo);
        }

        public async Task<Usuario?> ValidaEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<UsuarioGetAllReturn>> GetAllUsuario()
        {
            return await _dbSet
                .Include(u => u.NivelAcesso)
                .Where(u => u.Ativo)
                .Select(u => new UsuarioGetAllReturn { Id= u.Id, Nome = u.Nome, Email =u.Email,DataCriacao = u.Data, NivelAcesso = u.NivelAcesso.Nome })
                .ToListAsync();

        }

        public async Task<UsuarioGetByIdReturn?> GetUsuarioById(int id)
        {
            return await _dbSet
                .Where(u => u.Ativo)
                .Select(u => new UsuarioGetByIdReturn { Id = u.Id, Nome = u.Nome, Email = u.Email, DataCriacao = u.Data, NivelAcesso = u.NivelAcesso.Nome })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UsuarioMeReturn?> GetUsuarioByEmail(string email)
        {
            return await _dbSet
                .Where(u => u.Email == email)
                .Select(u => new UsuarioMeReturn { Id = u.Id, Nome = u.Nome, Email = u.Email, DataCriacao = u.Data, NivelAcesso = u.NivelAcesso.Nome })
                .FirstOrDefaultAsync();
        }
    }
}
