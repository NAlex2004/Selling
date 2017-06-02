using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace NAlex.Selling.DAL.Repositories
{
    public abstract class Repository<TEntity, TKey, TContext>: IRepository<TEntity, TKey>
        where TEntity: class
        where TContext: DbContext
    {
        private DbContext _context;

        public Repository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public TEntity Get(TKey Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToArray();
        }

        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> condition)
        {
            return _context.Set<TEntity>().Where(condition).ToArray();
        }

        public TEntity Add(TEntity entity)
        {
            if (entity == null)
                return null;

            return _context.Set<TEntity>().Add(entity);
        }

        public TEntity Remove(TEntity entity)
        {
            if (entity == null)
                return null;

            return _context.Set<TEntity>().Remove(entity);
        }

        public TEntity Remove(TKey Id)
        {
            TEntity entity = Get(Id);
            if (entity != null)
                return Remove(entity);

            return null;
        }

        public bool Update(TEntity entity)
        {
            if (entity == null)
                return false;

            _context.Set<TEntity>().Attach(entity);
            _context.Entry<TEntity>(entity).State = EntityState.Modified;

            return true;
        }
    }
}
