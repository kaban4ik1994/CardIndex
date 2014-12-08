﻿using System;
using System.Data.Entity;
using System.Linq;
using CardIndex.Context;
using CardIndex.DBInteractions.Interface;

namespace CardIndex.DBInteractions.Concrete
{
    public class EntityRepositoryBase<T> where T : class
    {
        private CardIndexContext _dataContext;
        private readonly IDbSet<T> _dbSet;

        protected EntityRepositoryBase(IDbFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbSet = DataContext.Set<T>();
        }

        protected IDbFactory DatabaseFactory { get; private set; }
        protected CardIndexContext DataContext { get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); } }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(Func<T, Boolean> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }

        public virtual T GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IQueryable<T> GetMany(Func<T, bool> where)
        {
            return _dbSet.Where(where).AsQueryable();
        }

        public T Get(Func<T, Boolean> where)
        {
            return _dbSet.Where(where).FirstOrDefault();
        }

    }
}
