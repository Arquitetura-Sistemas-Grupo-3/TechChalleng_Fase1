using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Input
{
    public class UsuarioUpdate
    {
        public required int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Senha { get; set; }

        public required int NivelAcessoId { get; set; } 
    }
}
