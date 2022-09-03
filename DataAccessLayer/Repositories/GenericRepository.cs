using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T>
        where T : class
    {
        public void Delete(T entity)
        {
            using (var context=new Context())
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public T GetByID(int id)
        {
            using (var context = new Context())
            {
                return context.Set<T>().Find(id);
            }
        }

        public List<T> GetListAll()
        {
            using (var context= new Context())
            {
                return context.Set<T>().ToList();
            }
        }

        public void Insert(T entity)
        {
            using (var context=new Context())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            using (var context = new Context())
            {
                return context.Set<T>().Where(filter).ToList();
            }
        }

        public void Update(T entity)
        {
            using (var context=new Context())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
