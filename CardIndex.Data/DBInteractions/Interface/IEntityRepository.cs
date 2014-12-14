using System;
using System.Linq;

namespace CardIndex.Data.DBInteractions.Interface
{
    public interface IEntityRepository<T> where T : class
    {
        void Add(T entity);
        void Attach(T entity);
        void Update(T entity);
        void Delete(T delete);
        void Delete(Func<T, Boolean> predicate);
        void Detach(T entity);
        T GetById(long id);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Func<T, bool> where);
    }
}
