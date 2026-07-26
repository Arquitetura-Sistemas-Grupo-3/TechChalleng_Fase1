using Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidade
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
        public int NivelAcessoId { get; set; }

        public ICollection<Jogo> Jogo{ get; set; }
        public NivelAcesso NivelAcesso { get; set; }

        public void Atualizar(UsuarioUpdate usuarioUpdate, string password)
        {
            Nome = usuarioUpdate.Nome;
            Email = usuarioUpdate.Email;
            Senha = password;
            NivelAcessoId = usuarioUpdate.NivelAcessoId;
        }
    }
}
