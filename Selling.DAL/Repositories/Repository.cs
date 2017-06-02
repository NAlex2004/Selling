using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace NAlex.Selling.DAL.Repositories
{
    public abstract class Repository<TEntity, TKey>: IRepository<TEntity, TKey>
        where TEntity: class
    {
        protected DbContext _context;

        public Repository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public virtual TEntity Get(TKey Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToArray();
        }

        public virtual IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var q =  _context.Set<TEntity>().Where(condition);
            
            if (orderBy != null)
                return orderBy(q).ToArray();

            return q.ToArray();
        }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
                return null;

            return _context.Set<TEntity>().Add(entity);
        }

        public virtual TEntity Remove(TEntity entity)
        {
            if (entity == null)
                return null;

            return _context.Set<TEntity>().Remove(entity);
        }

        public virtual TEntity Remove(TKey Id)
        {
            TEntity entity = Get(Id);
            if (entity != null)
                return Remove(entity);

            return null;
        }

        public virtual bool Update(TEntity entity)
        {
            if (entity == null)
                return false;

            //_context.Set<TEntity>().Attach(entity);
            _context.Entry<TEntity>(entity).State = EntityState.Modified;

            return true;
        }
    }
}
