using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentTracker.Repository.Testing {
    public class FakeRepository<T> : IRepository<T> where T : class {

        private readonly IList<T> _repo = new List<T>();

        public IQueryable<T> Collection {
            get { return _repo.AsQueryable(); }
        }

        public void Add(T item) {
            _repo.Add(item);
        }

        public void Delete(T item) {
            _repo.Remove(item);
        }

        public void Drop() {
            _repo.Clear();
        }

        public void Save(T item) {
            throw new NotImplementedException();
        }
    }
}
