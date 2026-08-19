using Core.Entidade;
using Core.Output;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Task<Usuario?> ValidaEmailSenha(string email);
        public Task<List<UsuarioReturn>> GetAllUsuario();
        public Task<UsuarioReturn?> GetUsuarioById(int id);
        public Task<Usuario?> ValidaEmail(string email);
        public Task<UsuarioReturn?> GetUsuarioByEmail(string email);
    }
}
