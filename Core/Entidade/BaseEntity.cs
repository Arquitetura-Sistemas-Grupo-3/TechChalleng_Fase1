using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidade
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime Data { get; set; } =  DateTime.Now;

        public bool Ativo { get; private set; } = true;

        public void Ativar()
        {
            Ativo = true;
        }
        public void Desativar()
        {
            Ativo = false;
        }
    }
}
