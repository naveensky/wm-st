using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using Norm.Linq;

namespace StudentTracker.Repository.MongoDb {

    public class MongoRepository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class, new() {

        private readonly Mongo _provider;

        public MongoRepository(ServerConfig config) {
            _provider = new Mongo(config.Database, config.ServerAddress, config.Port.ToString(), null);
        }
        public IQueryable<TEntity> Collection {
            get { return _provider.GetCollection<TEntity>().AsQueryable(); }
        }
        public void Add(TEntity item) {
            _provider.Database.GetCollection<TEntity>().Insert(item);
        }
        public void Dispose() {
            _provider.Dispose();
        }
        public void Delete(TEntity item) {
            _provider.Database.GetCollection<TEntity>().Delete(item);
        }
        public void Drop() {
            _provider.Database.DropCollection(typeof(TEntity).Name);
        }
        public void Save(TEntity item) {
            _provider.Database.GetCollection<TEntity>().Save(item);
        }
    }

}
