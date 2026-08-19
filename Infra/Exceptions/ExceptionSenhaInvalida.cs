using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Exceptions
{
    public class ExceptionSenhaInvalida : ExceptionBase
    {
        public ExceptionSenhaInvalida(string message) : base(message)
        {
        }
        public override int StatusCode { get; set; } = 400;
    
    }
}
