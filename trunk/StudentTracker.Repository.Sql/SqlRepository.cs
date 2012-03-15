using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Repository.Sql {
    public class SqlRepository<T> : IRepository<T> where T : class , IEntity {
        private readonly IDbSet<T> _dbSet;
        private readonly DbContext _dbContext;

        public SqlRepository(DbContext context) {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public T Add(T entity) {
            return _dbSet.Add(entity);
        }

        public void Remove(T entity) {
            _dbSet.Remove(entity);
            SaveChanges();
        }

        public IQueryable<T> Fetch(params System.Linq.Expressions.Expression<Func<T, object>>[] includes) {
            return _dbSet.IncludeMultiple(includes);
        }


        public IQueryable<TZ> Fetch<TZ>(params System.Linq.Expressions.Expression<Func<TZ, object>>[] includes) where TZ : class {
            return _dbSet.OfType<TZ>().IncludeMultiple(includes);
        }

        public IQueryable<T> Find(Func<T, bool> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] includes) {
            return _dbSet.IncludeMultiple(includes).Where(predicate).AsQueryable();
        }

        public IQueryable<TZ> Find<TZ>(Func<TZ, bool> predicate, params System.Linq.Expressions.Expression<Func<TZ, object>>[] includes) where TZ : class {
            return _dbSet.OfType<TZ>().IncludeMultiple(includes).Where(predicate).AsQueryable();
        }


        public T FindById(int id, params System.Linq.Expressions.Expression<Func<T, object>>[] includes) {
            return _dbSet.IncludeMultiple(includes).SingleOrDefault(x => x.Id == id);
        }

        public TZ FindById<TZ>(int id, params System.Linq.Expressions.Expression<Func<TZ, object>>[] includes) where TZ : class,IEntity {
            return _dbSet.OfType<TZ>().IncludeMultiple(includes).SingleOrDefault(x => x.Id == id);
        }

        public T Single(Func<T, bool> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] includes) {
            return _dbSet.IncludeMultiple(includes).SingleOrDefault(predicate);
        }

        public TZ Single<TZ>(Func<TZ, bool> predicate, params System.Linq.Expressions.Expression<Func<TZ, object>>[] includes) where TZ : class,IEntity {
            return _dbSet.OfType<TZ>().IncludeMultiple(includes).SingleOrDefault(predicate);
        }

        public void SaveChanges() {
            _dbContext.SaveChanges();
        }

       
    }
}
