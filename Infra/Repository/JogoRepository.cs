using Core.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class JogoRepository : EFRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(ApplicationDbContext applicationDb) : base(applicationDb)
        {
        }   
    
    }
}
