using Core.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        public Task<List<T>> GetAll();

        public Task<T> GetById(int id);

        public void Add(T entity);

        public void Update(T entity);

    }
}
