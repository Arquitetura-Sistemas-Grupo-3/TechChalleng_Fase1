using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidade
{
    public class UsuarioJogo : BaseEntity
    {
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public int JogoId { get; set; }

        public Jogo Jogo { get; set; }
    }
}
