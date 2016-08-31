using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCore;
using System.Data.Entity; 

namespace Repository
{
  public class Repository<TEntity> :IRepository<TEntity> where TEntity:class
    {

        public DataContext _appContex = null;
        public DbSet<TEntity> _entity = null;

        public Repository(DataContext Context)
        {
            _appContex = Context;
            _entity = _appContex.Set<TEntity>();
             
        }

        public IEnumerable<TEntity> Get()
        {
            var objList=_entity.ToList();
            return objList;
        }

        public TEntity  GetById(object id)
        {
            var objElement = _entity.Find(id);
            return objElement;

        }

        public void Add(TEntity entity)
           {
            _entity.Add(entity) ;

            }

        public void Update(TEntity entity)
        {
            _appContex.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            TEntity entityObject = _entity.Find(id);
            _entity.Remove(entityObject);
        }
    }
}
