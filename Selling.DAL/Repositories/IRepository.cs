﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NAlex.Selling.DAL.Repositories
{
    public interface IRepository<TEntity, in TKey>
        where TEntity: class
    {
        TEntity Get(TKey Id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> range);
        void Remove(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> condition);
        void Remove(TKey Id);
        bool Update(TEntity entity);
    }
}
