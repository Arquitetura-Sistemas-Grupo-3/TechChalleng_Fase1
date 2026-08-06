using Core.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Output
{
    public class UsuarioReturn
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }
        public string NivelAcesso { get; set; }
    }
}
