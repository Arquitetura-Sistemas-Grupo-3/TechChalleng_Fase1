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

        public virtual Usuario AddUsuario(UsuarioInput usuario, string? password)
        {
            return new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = password,
                NivelAcessoId = usuario.NivelAcessoId
            };
        }

        public virtual void Atualizar(Usuario usuarioAntigo,UsuarioUpdate usuarioNovo, string? password)
        {
            Nome = string.IsNullOrEmpty(usuarioNovo.Nome) ? usuarioAntigo.Nome : usuarioNovo.Nome;
            Email = string.IsNullOrEmpty(usuarioNovo.Email) ? usuarioAntigo.Email : usuarioNovo.Email;
            Senha = string.IsNullOrEmpty(password) ? usuarioAntigo.Senha : password;
            NivelAcessoId = usuarioNovo.NivelAcessoId == 0 ? usuarioAntigo.NivelAcessoId : usuarioNovo.NivelAcessoId;
        }
    }
}
