using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataCore;

namespace Repository
{
   public class UnitOfWork:IUnitOfWork
    {

        private readonly DataContext _appContext;
        private Dictionary<Type, object> dicRepositories = new Dictionary<Type, object>();
        public UnitOfWork()
        {
            _appContext = new DataContext();
        }
        public UnitOfWork(DataContext context)
        {
            _appContext = context;
        }
        public void Save()
        {
            _appContext.SaveChanges();    
        }
        public IRepository<T> Repository<T>() where T:class
        {

            if(dicRepositories.Keys.Contains(typeof(T))==true)
                {
                return dicRepositories[typeof(T)] as IRepository<T>;
                }

                 Repository<T> objRepository = new Repository<T>(_appContext);
                 dicRepositories.Add(typeof(T), objRepository);
                 return objRepository;               
        }


    }
}
