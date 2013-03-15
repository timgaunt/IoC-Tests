namespace IoCTests.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using IoCTests.Common.Interfaces;

    public class GenericRepository<TEntity> : IDisposable, IRepository<TEntity>
        where TEntity : class
    {
        protected DbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetQuery().AsEnumerable();
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().SingleOrDefault(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().FirstOrDefault(predicate);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().Any(predicate);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                return;
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                return;
            _dbSet.Remove(entity);
        }

        public void Attach(TEntity entity)
        {
            _dbSet.Attach(entity);
        }

        public void Update(TEntity entity)
        {
            var tracker = _context.Entry(entity);
            tracker.State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            OnDispose(true);
        }

        protected void OnDispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        protected IEnumerable<TEntity> GetFromRawSql(string sqlQuery, params object[] parameters)
        {
            return _dbSet.SqlQuery(sqlQuery, parameters).ToList();
        }
    }
}