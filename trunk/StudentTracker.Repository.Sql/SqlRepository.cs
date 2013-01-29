using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using StudentTracker.Models;

namespace StudentTracker.Repository.Sql {
    public class SqlRepository<T> : IRepository<T> where T : class , IEntity {
        private readonly IDbSet<T> dbSet;
        private readonly DbContext dbContext;

        public SqlRepository(DbContext context) {
            dbContext = context;
            dbSet = context.Set<T>();
        }

        public T Add(T entity) {
            return dbSet.Add(entity);
        }

        public void Remove(T entity) {
            dbSet.Remove(entity);
            SaveChanges();
        }

        public IQueryable<T> Fetch(params Expression<Func<T, object>>[] includes) {
            
            return dbSet.IncludeMultiple(includes);
        }


        public IQueryable<TZ> Fetch<TZ>(params Expression<Func<TZ, object>>[] includes) where TZ : class {
            return dbSet.OfType<TZ>().IncludeMultiple(includes);
        }

        public IQueryable<T> Find(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes) {
           
            return dbSet.IncludeMultiple(includes).Where(predicate).AsQueryable();
        }

        public IQueryable<TZ> Find<TZ>(Func<TZ, bool> predicate, params Expression<Func<TZ, object>>[] includes) where TZ : class {
            return dbSet.OfType<TZ>().IncludeMultiple(includes).Where(predicate).AsQueryable();
        }


        public T FindById(int id, params Expression<Func<T, object>>[] includes) {
            return dbSet.IncludeMultiple(includes).SingleOrDefault(x => x.Id == id);
        }

        public TZ FindById<TZ>(int id, params Expression<Func<TZ, object>>[] includes) where TZ : class,IEntity {
            return dbSet.OfType<TZ>().IncludeMultiple(includes).SingleOrDefault(x => x.Id == id);
        }

        public T Single(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes) {
            return dbSet.IncludeMultiple(includes).SingleOrDefault(predicate);
        }

        public TZ Single<TZ>(Func<TZ, bool> predicate, params Expression<Func<TZ, object>>[] includes) where TZ : class,IEntity {
            return dbSet.OfType<TZ>().IncludeMultiple(includes).SingleOrDefault(predicate);
        }

        public void SaveChanges() {
            dbContext.SaveChanges();
        }

       
    }
}
