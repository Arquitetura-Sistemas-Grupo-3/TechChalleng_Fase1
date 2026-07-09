using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidade
{
    public class Jogo : BaseEntity
    {
        public string Nome { get; set; }

        public ICollection<Usuario> Usuario { get; set; }   
    }
}
