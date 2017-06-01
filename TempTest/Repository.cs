using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;
using System.Linq.Expressions;

namespace TempTest
{
    interface IRepositiry<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int id);
        void Add(TEntity item);
        bool Remove(TEntity item);
        bool Remove(int id);
        bool Update(TEntity item);
    }


    public abstract class Repository<TEntity>: IRepositiry<TEntity>, IDisposable where TEntity: class
    {
        DbContext context;
        bool disposed = false;

        public Repository()
        {
            context = new SalesContext();
        }

        public void Dispose()
        {            
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                context.SaveChanges();
                context.Dispose();
            }

            disposed = true;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Add(TEntity item)
        {
            context.Set<TEntity>().Add(item);
        }

        public bool Remove(TEntity item)
        {
            context.Set<TEntity>().Remove(item);
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
