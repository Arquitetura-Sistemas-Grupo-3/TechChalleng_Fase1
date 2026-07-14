using Core.Entidade;
using Core.Repository;

namespace Infra.Repository
{
    public class NivelAcessoRepository : EFRepository<NivelAcesso>, INivelAcessoRepository
    {
        public NivelAcessoRepository(ApplicationDbContext applicationDb) 
            : base(applicationDb)
        {
        }
    }
}
