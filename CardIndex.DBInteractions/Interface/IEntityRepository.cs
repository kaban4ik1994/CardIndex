using System;
using System.Linq;

namespace CardIndex.DBInteractions.Interface
{
    public interface IEntityRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T delete);
        void Delete(Func<T, Boolean> predicate);
        T GetById(long id);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Func<T, bool> where);
    }
}
