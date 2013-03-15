namespace IoCTests.Common.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetQuery();

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        bool Any(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Attach(TEntity entity);

        void SaveChanges();
    }
}