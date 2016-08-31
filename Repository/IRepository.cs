using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity GetById(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object Id);
     

    }
}
