using Core.Input;
using Core.ValueObjects;
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

        public Email Email { get; set; }

        public string Senha { get; set; }
        public int NivelAcessoId { get; set; }

        public ICollection<Jogo> Jogo{ get; set; }
        public NivelAcesso NivelAcesso { get; set; }

        public virtual Usuario AdicionarUsuario(UsuarioAdicionarRequisicao usuario, int nivelAcessoId,string? password)
        {
            return new Usuario
            {
                Nome = usuario.Nome,
                Email = new Email(usuario.Email),
                Senha = password,
                NivelAcessoId = nivelAcessoId
            };
        }

        public virtual void Atualizar(Usuario usuarioAntigo,UsuarioAtualizarRequisicao usuarioNovo, string? password)
        {
            Nome = string.IsNullOrEmpty(usuarioNovo.Nome) ? usuarioAntigo.Nome : usuarioNovo.Nome;
            Email = string.IsNullOrEmpty(usuarioNovo.Email) ? usuarioAntigo.Email : new Email(usuarioNovo.Email);
            Senha = string.IsNullOrEmpty(password) ? usuarioAntigo.Senha : password;
            NivelAcessoId = usuarioNovo.NivelAcessoId == 0 ? usuarioAntigo.NivelAcessoId : usuarioNovo.NivelAcessoId;
        }
    }
}
