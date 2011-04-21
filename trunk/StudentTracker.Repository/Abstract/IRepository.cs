using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracker.Repository {
    public interface IRepository<T> where T : class {
        IQueryable<T> Collection { get; }
        void Add(T item);
        void Delete(T item);
        void Drop();
        void Save(T item);
    }
}
