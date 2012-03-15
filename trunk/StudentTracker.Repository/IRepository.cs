using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using StudentTracker.Models;

namespace StudentTracker.Repository {
    public interface IRepository<T> where T : class {
        T Add(T entity);
        void Remove(T entity);
        IQueryable<T> Fetch(params Expression<Func<T, object>>[] includes);
        IQueryable<TZ> Fetch<TZ>(params Expression<Func<TZ, object>>[] includes)
            where TZ : class;

        IQueryable<T> Find(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);

        IQueryable<TZ> Find<TZ>(Func<TZ, bool> predicate,
            params Expression<Func<TZ, object>>[] includes) where TZ : class;

        T FindById(int id, params Expression<Func<T, object>>[] includes);

        TZ FindById<TZ>(int id, params Expression<Func<TZ, object>>[] includes)
            where TZ : class, IEntity;

        T Single(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);
        void SaveChanges();

        TZ Single<TZ>(Func<TZ, bool> predicate, params System.Linq.Expressions.Expression<Func<TZ, object>>[] includes)
            where TZ : class, IEntity;

        


    }
}
